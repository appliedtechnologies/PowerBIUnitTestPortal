import { Component, OnInit, ViewChild } from "@angular/core";
import { DxDataGridComponent, DxTreeListComponent } from "devextreme-angular";
import CustomStore from "devextreme/data/custom_store";
import DataSource from "devextreme/data/data_source";
import { ClickEvent } from "devextreme/ui/button";
import { ToolbarPreparingEvent } from "devextreme/ui/tree_list";
import { resolve } from "dns";
import { UnitTestService } from "src/app/shared/services/UnitTest.service";
import {
    LayoutParameter,
    LayoutService,
    NotificationType,
} from "src/app/shared/services/layout.service";
import { WorkspaceService } from "src/app/shared/services/workspace.service";

@Component({
    selector: "app-unit-tests-new",
    templateUrl: "./unit-tests-new.component.html",
    styleUrls: ["./unit-tests-new.component.css"],
})
export class UnitTestsNewComponent implements OnInit {
    public dataSourceWorkspaces: DataSource;
    @ViewChild(DxTreeListComponent, { static: false })
    treeList: DxDataGridComponent;

    constructor(
        private unitTestService: UnitTestService,
        private workspaceService: WorkspaceService,
        private layoutService: LayoutService
    ) {
        this.dataSourceWorkspaces = new DataSource({
            store: new CustomStore({
                key: "fakeId",
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
                            let fakeIdWorkspace = Math.floor(Math.random() * (Number.MAX_VALUE - 1 + 1));
                            e["fakeId"] = fakeIdWorkspace;
                            e["items"]?.forEach(ee => {
                                delete Object.assign(ee, { ["items"]: ee["UserStories"] })["UserStories"]
                                ee["type"] = "Tabular Model";
                                ee["parentId"] = fakeIdWorkspace;
                                let fakeIdUserStory = Math.floor(Math.random() * (Number.MAX_VALUE - 1 + 1));
                                ee["fakeId"] = fakeIdUserStory;
                                ee["items"]?.forEach(eee => {
                                    delete Object.assign(eee, { ["items"]: eee["UnitTests"] })["UnitTests"]
                                    eee["type"] = "User Story";
                                    eee["parentId"] = fakeIdUserStory;
                                    let fakeIdUnitTest = Math.floor(Math.random() * (Number.MAX_VALUE - 1 + 1));
                                    eee["fakeId"] = fakeIdUnitTest;
                                    eee["items"]?.forEach(eeee => {
                                        eeee["type"] = "Unit Test";
                                        eeee["parentId"] = fakeIdUserStory;
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

    public onClickRefresh(): void {
        this.treeList.instance.refresh();
    }

}
