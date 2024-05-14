import { Component, OnInit, ViewChild } from '@angular/core';
import { DxDataGridComponent } from 'devextreme-angular';
import DataSource from 'devextreme/data/data_source';
import { ClickEvent } from 'devextreme/ui/button';
import { ToolbarPreparingEvent } from 'devextreme/ui/data_grid';
import { LayoutParameter, LayoutService, NotificationType } from 'src/app/shared/services/layout.service';
import { WorkspaceService } from 'src/app/shared/services/workspace.service';

@Component({
    selector: 'app-workspaces',
    templateUrl: './workspaces.component.html',
    styleUrls: ['./workspaces.component.css']
})
export class WorkspacesComponent {

    public dataSourceWorkspaces: DataSource;

    @ViewChild(DxDataGridComponent, { static: false })
    dataGrid: DxDataGridComponent;

    constructor(private workspaceService: WorkspaceService, private layoutService: LayoutService) {
        this.dataSourceWorkspaces = new DataSource({
            store: workspaceService.getStore()
        });
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
                icon: "download",
                text: "Pull Workspaces",
                stylingMode: "contained",
                type: "normal",
                onClick: this.onClickPullWorkspaces.bind(this),
            },
            location: "before",
        });
    }

    
    public onClickPullWorkspaces(e: ClickEvent): void {
        this.layoutService.change(LayoutParameter.ShowLoading, true);
        this.workspaceService
            .pullWorkspaces()
            .then(() => {
                this.dataGrid.instance.refresh();
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

    public onClickRefresh(e: ClickEvent): void {
        this.dataGrid.instance.refresh();
    }
}