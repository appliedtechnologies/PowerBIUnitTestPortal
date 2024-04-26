import { Component, OnInit, ViewChild } from "@angular/core";
import { DxDataGridComponent } from "devextreme-angular";
import DataSource from "devextreme/data/data_source";
import { ClickEvent } from "devextreme/ui/button";
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
  public dataSourceUnitTests: DataSource;
  @ViewChild(DxDataGridComponent, { static: false })
  datagrid: DxDataGridComponent;

  constructor(
    private unitTestService: UnitTestService,
    private workspaceService: WorkspaceService,
    private layoutService: LayoutService
  ) {
    this.dataSourceUnitTests = new DataSource({
      store: this.unitTestService.getStore(),
    });
  }

  ngOnInit(): void {}

  public onClickPullWorkspaces($event: ClickEvent) {
    this.layoutService.change(LayoutParameter.ShowLoading, true);
    this.workspaceService
      .pullWorkspaces()
      .then(() => {
        this.datagrid.instance.refresh();
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
}
