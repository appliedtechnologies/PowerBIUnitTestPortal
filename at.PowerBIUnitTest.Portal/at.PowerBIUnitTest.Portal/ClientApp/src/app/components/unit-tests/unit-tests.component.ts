import { Component, OnInit, ViewChild } from "@angular/core";
import { DxDataGridComponent, DxTreeListComponent } from "devextreme-angular";
import CustomStore from "devextreme/data/custom_store";
import DataSource from "devextreme/data/data_source";
import { ClickEvent } from "devextreme/ui/button";
import { ToolbarPreparingEvent } from "devextreme/ui/tree_list";
import { UserStory } from "src/app/shared/models/user-story.model";
import { UnitTestService } from "src/app/shared/services/unit-test.service";
import { confirm } from 'devextreme/ui/dialog';
import {
    LayoutParameter,
    LayoutService,
    NotificationType,
} from "src/app/shared/services/layout.service";
import { UserStoryService } from "src/app/shared/services/user-story.service";
import { WorkspaceService } from "src/app/shared/services/workspace.service";
import { UnitTest } from "src/app/shared/models/unit-test.model";
import ODataContext from "devextreme/data/odata/context";
import { TabularModelService } from "src/app/shared/services/tabular-model.service";

@Component({
    selector: "app-unit-tests",
    templateUrl: "./unit-tests.component.html",
    styleUrls: ["./unit-tests.component.css"],
})
export class UnitTestsComponent implements OnInit {
    public dataSourceWorkspaces: DataSource;
    public dataSourceWorkspacesOdata: DataSource;
    public dataSourceTabularModels: DataSource;
    @ViewChild(DxTreeListComponent, { static: false })
    treeList: DxDataGridComponent;

    public isVisibleEditUnitTest: boolean = false;
    public isVisibleEditUserStory: boolean = false;
    public isVisibleCopyUserStory: boolean = false;
    public isVisibleTestRunHistory: boolean = false;
    public popupTitle: string = "";

    public userStoryToEdit: UserStory = {};
    public unitTestToEdit: UnitTest = {};
    public copyUserStoryConfig: { workspaceId?: number, tabularModelId?: number, userStoryId?: number, tabularModelToExclude?: number } = {};

    public resultTypeItems: string[] = ["String", "Number", "Date", "Percentage"];
    public dateTimeFormatItems: string[] = ["UTC", "CET"];
    public floatSeparatorItems: string[] = ["Use Seperators", "Dont use Sperators"];
    public decimalPlacesItems: string[] = ["0", "1", "2", "3", "4", "5"];

    constructor(
        private unitTestService: UnitTestService,
        private userStoryService: UserStoryService,
        private workspaceService: WorkspaceService,
        private tabularModelService: TabularModelService,
        private layoutService: LayoutService
    ) {
        this.onClickExecuteUnitTests = this.onClickExecuteUnitTests.bind(this);
        this.onClickAddUserStory = this.onClickAddUserStory.bind(this);
        this.onClickDeleteWorkspace = this.onClickDeleteWorkspace.bind(this);
        this.onClickDeleteTabularModel = this.onClickDeleteTabularModel.bind(this);
        this.onClickEditUserStory = this.onClickEditUserStory.bind(this);
        this.onClickDeleteUserStory = this.onClickDeleteUserStory.bind(this);
        this.onClickAddUnitTest = this.onClickAddUnitTest.bind(this);
        this.onClickEditUnitTest = this.onClickEditUnitTest.bind(this);
        this.onClickDeleteUnitTest = this.onClickDeleteUnitTest.bind(this);
        this.onClickCopyUserStory = this.onClickCopyUserStory.bind(this);
        this.onClickShowRunHistory = this.onClickShowRunHistory.bind(this);

        this.dataSourceWorkspaces = new DataSource({
            store: new CustomStore({
                key: "UniqueIdentifier",
                load: async (loadOptions) => {
                    if (loadOptions.expand == null)
                        loadOptions.expand = new Array();
                    loadOptions.expand.push("TabularModels");
                    loadOptions.expand.push("TabularModels.UserStories");
                    loadOptions.expand.push("TabularModels.UserStories.UnitTests");
                    loadOptions.expand.push("TabularModels.UserStories.UnitTests.TestRuns($top=1;$orderby=TimeStamp desc)");
                    return (this.workspaceService.getStore().load(loadOptions)).then((data) => {
                        data.forEach(e => {
                            delete Object.assign(e, { ["items"]: e["TabularModels"] })["TabularModels"]
                            e["type"] = "Workspace";
                            e["parentId"] = 0;
                            e["items"]?.forEach(ee => {
                                delete Object.assign(ee, { ["items"]: ee["UserStories"] })["UserStories"]
                                ee["type"] = "Tabular Model";
                                ee["parentId"] = e["UniqueIdentifier"];
                                ee["items"]?.forEach(eee => {
                                    delete Object.assign(eee, { ["items"]: eee["UnitTests"] })["UnitTests"]
                                    eee["type"] = "User Story";
                                    eee["parentId"] = ee["UniqueIdentifier"];
                                    eee["items"]?.forEach(eeee => {
                                        eeee["type"] = "Unit Test";
                                        eeee["parentId"] = eee["UniqueIdentifier"];
                                    })
                                })
                            })
                        });
                    });
                }
            })
        });

        this.dataSourceWorkspacesOdata = new DataSource({
            store: this.workspaceService.getStore(),
            sort: [{ selector: "Name", desc: false }]
        });
    }

    ngOnInit(): void { }

    public onClickPullWorkspaces(e: ClickEvent): void {
        this.layoutService.change(LayoutParameter.ShowLoading, true);
        this.workspaceService
            .pullWorkspaces()
            .then(() => {
                this.treeList.instance.refresh();
                this.layoutService.notify({
                    message: "Workspaces pulled successfully",
                    type: NotificationType.Success,
                });
            })
            .catch((error: Error) => this.layoutService.notify({
                message: error.message ? `Can not pull workspaces: ${error.message}` : "Error while pulling workspaces",
                type: NotificationType.Error,
            }))
            .finally(() =>
                this.layoutService.change(LayoutParameter.ShowLoading, false)
            );
    }

    public onToolbarPreparingTreeList(e: ToolbarPreparingEvent): void {
        let toolbarItems = e.toolbarOptions.items;

        toolbarItems.unshift({
            widget: "dxButton",
            options: {
                icon: "refresh",
                stylingMode: "contained",
                type: "normal",
                hint: "Refresh Applications",
                onClick: this.onClickRefresh.bind(this),
            },
            location: "after",
        });

        toolbarItems.unshift({
            widget: "dxButton",
            options: {
                icon: "clear",
                stylingMode: "contained",
                type: "normal",
                hint: "Clear Selection",
                onClick: this.onClickClearSelection.bind(this),
            },
            location: "after",
        });

        toolbarItems.unshift({
            widget: "dxButton",
            options: {
                icon: "trash",
                stylingMode: "contained",
                type: "danger",
                hint: "Delete Unit Tests",
                text: "Delete Unit Tests",
                onClick: this.onClickDeleteMultipleUnitTests.bind(this),
            },
            location: "after",
        });

        toolbarItems.unshift({
            widget: "dxButton",
            options: {
                icon: "runner",
                stylingMode: "contained",
                type: "success",
                hint: "Execute Unit Tests",
                text: "Execute Unit Tests",
                onClick: this.onClickExecuteMultipleUnitTests.bind(this),
            },
            location: "after",
        });

        toolbarItems.unshift({
            widget: "dxButton",
            options: {
                icon: "download",
                text: "Pull Workspaces",
                stylingMode: "contained",
                type: "normal",
                onClick: this.onClickPullWorkspaces.bind(this),
            },
            location: "before",
        });
    }

    public onCellPreparedTreeList(e: any): void {
        if (e.rowType === "data" && e.row.data.type == "Unit Test" && e.column.dataField === "TestRuns[0].Result") {
            e.cellElement.style.color = e.row.data?.TestRuns[0]?.WasPassed ? "green" : "red";
        }
    }

    public onRowDbClickTreeList(e: any): void {
        if (e.rowType === "data" && e.data.type == "Unit Test") {
            this.openEditUnitTestPopup(e.data);
        }
        else if (e.rowType == "data" && e.data.type == "User Story") {
            this.openEditUserStoryPopup(e.data);
        }
    }

    public onRowPreparedTreeList(e: any): void {
        if (e.rowType == "data" && e.data.type != "Unit Test" || e.rowType == "header") {
            e.rowElement.classList.add("hide-checkbox");
        }
    }

    public onClickShowRunHistory(e: any): void {
        this.popupTitle = "Test Run History: " + e.row.data.Name;
        this.unitTestToEdit = e.row.data;
        this.isVisibleTestRunHistory = true;
    }

    public onClickCopyUserStory(e: any): void {
        this.copyUserStoryConfig = {};
        this.copyUserStoryConfig.tabularModelToExclude = e.row.data.TabularModel;
        this.copyUserStoryConfig.userStoryId = e.row.data.Id;
        this.isVisibleCopyUserStory = true;
    }

    public onClickSaveCopyUserStory(e: any): void {
        this.layoutService.change(LayoutParameter.ShowLoading, true);
        this.userStoryService.copyToOtherTabularModel(this.copyUserStoryConfig.userStoryId, this.copyUserStoryConfig.tabularModelId)
            .then(() => {
                this.layoutService.notify({
                    type: NotificationType.Success,
                    message: "The user story was copied successfully."
                });
                this.isVisibleCopyUserStory = false;
                this.copyUserStoryConfig = {};
                this.treeList.instance.refresh();
            })
            .catch((error: Error) => this.layoutService.notify({
                type: NotificationType.Error,
                message: error?.message ? `The user story could not be copied.: ${error.message}` : "The user story could not be copied."
            }))
            .then(() => { this.layoutService.change(LayoutParameter.ShowLoading, false); });
    }

    public onValueChangeCopyUserStoryWorkspace(e: any): void {
        this.copyUserStoryConfig.workspaceId = e.value.Id;
        this.dataSourceTabularModels = new DataSource({
            store: this.tabularModelService.getStore(),
            filter: [["Workspace", "=", e.value.Id], "and", ["Id", "<>", this.copyUserStoryConfig.tabularModelToExclude]],
            sort: [{ selector: "Name", desc: false }],
        });
    }

    public onValueChangeCopyUserStoryTabularModel(e: any): void {
        this.copyUserStoryConfig.tabularModelId = e.value.Id;
    }

    public onClickClearSelection(e: any): void {
        this.treeList.instance.clearSelection();
    }

    public onClickExecuteMultipleUnitTests(e: any): void {
        let data = this.treeList.instance.getSelectedRowsData();
        let unitTestIds = data.filter((e: any) => e.type == "Unit Test").map((e: any) => e.Id);
        if (unitTestIds.length == 0) {
            this.layoutService.notify({
                type: NotificationType.Info,
                message: "No unit tests selected for execution."
            });
            return;
        }
        this.executeUnitTest(unitTestIds);
    }

    public onClickExecuteUnitTests(e: any): void {
        let unitTestIdsToExecute = [];
        if (e.row.data.type == "Unit Test")
            unitTestIdsToExecute = [e.row.data.Id];
        else if (e.row.data.type == "User Story")
            unitTestIdsToExecute = e.row.node.children.map((e: any) => e.data.Id);
        else if (e.row.data.type == "Tabular Model")
            unitTestIdsToExecute = e.row.node.children.flatMap((e: any) => e.children.map((ee: any) => ee.data.Id));
        else if (e.row.data.type == "Workspace")
            unitTestIdsToExecute = e.row.node.children.flatMap((e: any) => e.children.flatMap((ee: any) => ee.children.map((eee: any) => eee.data.Id)));

        this.executeUnitTest(unitTestIdsToExecute);
    }

    public onClickRefresh(e: ClickEvent): void {
        this.treeList.instance.refresh();
    }

    public onClickAddUnitTest(e: any): void {
        this.popupTitle = "Add Unit Test";
        this.unitTestToEdit = {};
        this.unitTestToEdit.UserStory = e.row.data.Id;
        this.isVisibleEditUnitTest = true;
    }

    public onClickEditUnitTest(e: any): void {
        this.openEditUnitTestPopup(e.row.data);
    }

    public onClickAddUserStory(e: any): void {
        this.popupTitle = "Add User Story";
        this.userStoryToEdit = {};
        this.userStoryToEdit.TabularModel = e.row.data.Id;
        this.isVisibleEditUserStory = true;
    }

    public onClickEditUserStory(e: any): void {
        this.openEditUserStoryPopup(e.row.data);
    }

    public onClickDeleteWorkspace(e: any): void {
        let result = confirm("Are you sure you want to delete this workspace (including all tabular models, user stories and unit tests)? If it continues to exist in Power BI, it will be created again with the next pull action.", "Delete Workspace");
        result.then((dialogResult) => {
            if (dialogResult) {
                this.layoutService.change(LayoutParameter.ShowLoading, true);
                this.workspaceService.remove(e.row.data.Id)
                    .then(() => this.layoutService.notify({
                        type: NotificationType.Success,
                        message: "The workspace was deleted successfully."
                    }))
                    .catch((error: Error) => this.layoutService.notify({
                        type: NotificationType.Error,
                        message: error?.message ? `The workspace could not be deleted: ${error.message}` : "The workspace could not be deleted."
                    }))
                    .then(() => {
                        this.treeList.instance.refresh();
                        this.layoutService.change(LayoutParameter.ShowLoading, false);
                    });
            }
        });
    }

    public onClickDeleteTabularModel(e: any): void {
        let result = confirm("Are you sure you want to delete this tabular model (including all user stories and unit tests)? If it continues to exist in Power BI, it will be created again with the next pull action.", "Delete Tabular Model");
        result.then((dialogResult) => {
            if (dialogResult) {
                this.layoutService.change(LayoutParameter.ShowLoading, true);
                this.tabularModelService.remove(e.row.data.Id)
                    .then(() => this.layoutService.notify({
                        type: NotificationType.Success,
                        message: "The tabular model was deleted successfully."
                    }))
                    .catch((error: Error) => this.layoutService.notify({
                        type: NotificationType.Error,
                        message: error?.message ? `The tabular model could not be deleted: ${error.message}` : "The user story could not be deleted."
                    }))
                    .then(() => {
                        this.treeList.instance.refresh();
                        this.layoutService.change(LayoutParameter.ShowLoading, false);
                    });
            }
        });
    }

    public onClickDeleteUserStory(e: any): void {
        let result = confirm("Are you sure you want to delete this user story (including all unit tests)?", "Delete User Story");
        result.then((dialogResult) => {
            if (dialogResult) {
                this.layoutService.change(LayoutParameter.ShowLoading, true);
                this.userStoryService.remove(e.row.data.Id)
                    .then(() => this.layoutService.notify({
                        type: NotificationType.Success,
                        message: "The user story was deleted successfully."
                    }))
                    .catch((error: Error) => this.layoutService.notify({
                        type: NotificationType.Error,
                        message: error?.message ? `The user story could not be deleted: ${error.message}` : "The user story could not be deleted."
                    }))
                    .then(() => {
                        this.treeList.instance.refresh();
                        this.layoutService.change(LayoutParameter.ShowLoading, false);
                    });
            }
        });
    }

    public onClickDeleteMultipleUnitTests(e: any): void {
        let data = this.treeList.instance.getSelectedRowsData();
        let unitTestIds = data.filter((e: any) => e.type == "Unit Test").map((e: any) => e.Id);
        if (unitTestIds.length == 0) {
            this.layoutService.notify({
                type: NotificationType.Info,
                message: "No unit tests selected for deletion."
            });
            return;
        }

        let result = confirm("Are you sure you want to delete these unit tests?", "Delete Unit Tests");
        result.then((dialogResult) => {
            if (dialogResult) {
                this.layoutService.change(LayoutParameter.ShowLoading, true);
                let promises = [];
                unitTestIds.forEach((e: any) => {
                    promises.push(this.unitTestService.remove(e));
                });
                Promise.all(promises)
                    .then(() => this.layoutService.notify({
                        type: NotificationType.Success,
                        message: "The unit tests were deleted successfully."
                    }))
                    .catch((error: Error) => this.layoutService.notify({
                        type: NotificationType.Error,
                        message: error?.message ? `One or more unit tests could not be deleted: ${error.message}` : "One or more unit tests could not be deleted"
                    }))
                    .then(() => {
                        this.treeList.instance.refresh();
                        this.layoutService.change(LayoutParameter.ShowLoading, false);
                    });
            }
        });
    }

    public onClickDeleteUnitTest(e: any): void {
        let result = confirm("Are you sure you want to delete this unit test?", "Delete Unit Test");
        result.then((dialogResult) => {
            if (dialogResult) {
                this.layoutService.change(LayoutParameter.ShowLoading, true);
                this.unitTestService.remove(e.row.data.Id)
                    .then(() => this.layoutService.notify({
                        type: NotificationType.Success,
                        message: "The unit test was deleted successfully."
                    }))
                    .catch((error: Error) => this.layoutService.notify({
                        type: NotificationType.Error,
                        message: error?.message ? `The unit test could not be deleted: ${error.message}` : "The unit test could not be deleted."
                    }))
                    .then(() => {
                        this.treeList.instance.refresh();
                        this.layoutService.change(LayoutParameter.ShowLoading, false);
                    });
            }
        });
    }

    public onClickSaveUnitTest(e: ClickEvent): void {
        let validation = e.validationGroup.validate();
        if (validation.isValid == true) {
            this.layoutService.change(LayoutParameter.ShowLoading, true);
            let editPromise;
            if (this.unitTestToEdit.Id != null) {
                editPromise = this.unitTestService.update(this.unitTestToEdit.Id, { Name: this.unitTestToEdit.Name, DAX: this.unitTestToEdit.DAX, ExpectedResult: this.unitTestToEdit.ExpectedResult, ResultType: this.unitTestToEdit.ResultType, DateTimeFormat: this.unitTestToEdit.DateTimeFormat, FloatSeparators: this.unitTestToEdit.FloatSeparators, DecimalPlaces: this.unitTestToEdit.DecimalPlaces })
                    .then(() => {
                        this.layoutService.notify({
                            type: NotificationType.Success,
                            message: "The unit test has been edited successfully."
                        });
                        this.isVisibleEditUnitTest = false;
                        this.unitTestToEdit = {};
                        this.treeList.instance.refresh();
                    })
                    .catch((error: Error) => this.layoutService.notify({
                        type: NotificationType.Error,
                        message: error?.message ? `The unit test could not be edited: ${error.message}` : "The unit test could not be edited."
                    }))

            }
            else {
                editPromise = this.unitTestService.add(this.unitTestToEdit)
                    .then(() => {
                        this.layoutService.notify({
                            type: NotificationType.Success,
                            message: "The new uni test has been created successfully."
                        });
                        this.isVisibleEditUnitTest = false;
                        this.unitTestToEdit = {};
                        this.treeList.instance.refresh();
                    })
                    .catch((error: Error) => this.layoutService.notify({
                        type: NotificationType.Error,
                        message: error?.message ? `The new unit test could not be created: ${error.message}` : "The new unit test could not be created."
                    }))
            }

            editPromise.then(() => {
                this.layoutService.change(LayoutParameter.ShowLoading, false);
            });
        }
    }

    public onClickSaveUserStory(e: ClickEvent): void {
        let validation = e.validationGroup.validate();
        if (validation.isValid == true) {
            this.layoutService.change(LayoutParameter.ShowLoading, true);
            let editPromise;
            if (this.userStoryToEdit.Id != null) {
                editPromise = this.userStoryService.update(this.userStoryToEdit.Id, { Name: this.userStoryToEdit.Name })
                    .then(() => {
                        this.layoutService.notify({
                            type: NotificationType.Success,
                            message: "The user story has been edited successfully."
                        });
                        this.isVisibleEditUserStory = false;
                        this.userStoryToEdit = {};
                        this.treeList.instance.refresh();
                    })
                    .catch((error: Error) => this.layoutService.notify({
                        type: NotificationType.Error,
                        message: error?.message ? `The user story could not be edited: ${error.message}` : "The user story could not be edited."
                    }))
            }
            else {
                editPromise = this.userStoryService.add(this.userStoryToEdit)
                    .then(() => {
                        this.layoutService.notify({
                            type: NotificationType.Success,
                            message: "The new user story has been created successfully."
                        });
                        this.isVisibleEditUserStory = false;
                        this.userStoryToEdit = {};
                        this.treeList.instance.refresh();
                    })
                    .catch((error: Error) => this.layoutService.notify({
                        type: NotificationType.Error,
                        message: error?.message ? `The new user story could not be created: ${error.message}` : "The new user story could not be created."
                    }))
            }

            editPromise.then(() => {
                this.layoutService.change(LayoutParameter.ShowLoading, false);
            });
        }
    }

    public isWorkspaceRow(e: any): boolean {
        if (e.row.rowType == "data" && e.row.data.type == "Workspace")
            return true;
        return false;
    }

    public isUnitTestRow(e: any): boolean {
        if (e.row.rowType == "data" && e.row.data.type == "Unit Test")
            return true;
        return false;
    }

    public isUserStoryRow(e: any): boolean {
        if (e.row.rowType == "data" && e.row.data.type == "User Story")
            return true;
        return false;
    }

    public isTabularModelRow(e: any): boolean {
        if (e.row.rowType == "data" && e.row.data.type == "Tabular Model")
            return true;
        return false;
    }

    private openEditUnitTestPopup(unitTest: UnitTest): void {
        this.popupTitle = "Edit Unit Test";
        this.unitTestToEdit = structuredClone(unitTest);
        this.isVisibleEditUnitTest = true;
    }

    private openEditUserStoryPopup(userStory: UserStory): void {
        this.popupTitle = "Edit User Story";
        this.userStoryToEdit = structuredClone(userStory);
        this.isVisibleEditUserStory = true;
    }

    private executeUnitTest(ids: number[]): void {
        this.layoutService.change(LayoutParameter.ShowLoading, true);
        this.unitTestService.executeMultiple(ids)
            .then(() => {
                this.layoutService.notify({
                    type: NotificationType.Success,
                    message: "Unit test(s) executed successfully."
                });
                this.treeList.instance.refresh();
            })
            .catch((error: Error) => this.layoutService.notify({
                type: NotificationType.Error,
                message: error?.message ? `Unit tests could not be executed: ${error.message}` : "Unit tests could not be executed."
            }))
            .then(() => { this.layoutService.change(LayoutParameter.ShowLoading, false); });
    }
}
