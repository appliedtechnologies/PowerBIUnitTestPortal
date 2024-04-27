import { Component, OnInit, ViewChild } from "@angular/core";
import { DxDataGridComponent, DxTreeListComponent } from "devextreme-angular";
import CustomStore from "devextreme/data/custom_store";
import DataSource from "devextreme/data/data_source";
import { ClickEvent } from "devextreme/ui/button";
import { ToolbarPreparingEvent } from "devextreme/ui/tree_list";
import { resolve } from "dns";
import { UserStory } from "src/app/shared/models/user-story.model";
import { UnitTestService } from "src/app/shared/services/unit-test.service";
import {
    LayoutParameter,
    LayoutService,
    NotificationType,
} from "src/app/shared/services/layout.service";
import { UserStoryService } from "src/app/shared/services/user-story.service";
import { WorkspaceService } from "src/app/shared/services/workspace.service";

@Component({
    selector: "app-unit-tests",
    templateUrl: "./unit-tests.component.html",
    styleUrls: ["./unit-tests.component.css"],
})
export class UnitTestsComponent implements OnInit {
    public dataSourceWorkspaces: DataSource;
    @ViewChild(DxTreeListComponent, { static: false })
    treeList: DxDataGridComponent;

    public isVisibleAddUnitTest: boolean = false;
    public isVisibleAddUserStory: boolean = false;

    public newUserStory: UserStory = {};

    constructor(
        private unitTestService: UnitTestService,
        private userStoryService: UserStoryService,
        private workspaceService: WorkspaceService,
        private layoutService: LayoutService
    ) {
        this.onClickAddUserStory = this.onClickAddUserStory.bind(this);
        this.onClickDeleteUnitTest = this.onClickDeleteUnitTest.bind(this);
        this.onClickDeleteUserStory = this.onClickDeleteUserStory.bind(this);

        this.dataSourceWorkspaces = new DataSource({
            store: new CustomStore({
                key: "UniqueIdentifier",
                load: async (loadOptions) => {
                    if (loadOptions.expand == null)
                        loadOptions.expand = new Array();
                    loadOptions.expand.push("TabularModels");
                    loadOptions.expand.push("TabularModels.UserStories");
                    loadOptions.expand.push("TabularModels.UserStories.UnitTests");
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
                type: "success",
                hint: "Refresh Applications",
                onClick: this.onClickRefresh.bind(this),
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

    public onClickRefresh(e: ClickEvent): void {
        this.treeList.instance.refresh();
    }

    public onClickAddUnitTest(e: ClickEvent): void { 
        this.isVisibleAddUnitTest = true;
    }

    public onClickAddUserStory(e: any): void { 
        this.newUserStory.TabularModel = e.row.data.Id;
        this.isVisibleAddUserStory = true;
    }

    public onClickDeleteUserStory(e: any): void{
        this.layoutService.change(LayoutParameter.ShowLoading, true);
        this.userStoryService.remove(e.row.data.Id)
          .then(() => this.layoutService.notify({
            type: NotificationType.Success,
            message: "The user story was deleted successfully."
          }))
          .catch((error: Error) => this.layoutService.notify({
            type: NotificationType.Error,
            message: error.message ? `The user story could not be deleted: ${error.message}` : "The user story could not be deleted."
          }))
          .then(() => {
            this.treeList.instance.refresh();
            this.layoutService.change(LayoutParameter.ShowLoading, false);
          });
    }

    public onClickDeleteUnitTest(e: any): void{
        this.layoutService.change(LayoutParameter.ShowLoading, true);
        this.unitTestService.remove(e.row.data.Id)
          .then(() => this.layoutService.notify({
            type: NotificationType.Success,
            message: "The unit test was deleted successfully."
          }))
          .catch((error: Error) => this.layoutService.notify({
            type: NotificationType.Error,
            message: error.message ? `The unit test could not be deleted: ${error.message}` : "The unit test could not be deleted."
          }))
          .then(() => {
            this.treeList.instance.refresh();
            this.layoutService.change(LayoutParameter.ShowLoading, false);
          });
    }

    public onClickSaveNewUserStory(e: ClickEvent): void {
        this.layoutService.change(LayoutParameter.ShowLoading, true);
        this.userStoryService.add(this.newUserStory)
          .then(() => this.layoutService.notify({
            type: NotificationType.Success,
            message: "The new user story has been created successfully."
          }))
          .catch((error: Error) => this.layoutService.notify({
            type: NotificationType.Error,
            message: error.message ? `The new user story could not be created: ${error.message}` : "The new user story could not be created."
          }))
          .then(() => {
            this.isVisibleAddUserStory = false;
            this.newUserStory = {};
            this.treeList.instance.refresh();
            this.layoutService.change(LayoutParameter.ShowLoading, false);
          });
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

}
