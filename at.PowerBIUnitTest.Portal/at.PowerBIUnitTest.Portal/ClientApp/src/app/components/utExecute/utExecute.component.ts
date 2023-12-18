import { Component, OnInit, ViewChild } from "@angular/core";
import DataSource from "devextreme/data/data_source";
import { UserService } from "src/app/shared/services/user.service";
import {
  LayoutParameter,
  LayoutService,
  NotificationType,
} from "src/app/shared/services/layout.service";
import { AppConfig } from "src/app/shared/config/app.config";
import { User } from "src/app/shared/models/user.model";
import { environment } from "src/environments/environment";
import { DxDataGridComponent, DxListComponent, DxPopupComponent } from "devextreme-angular";
import { UnitTestService } from "src/app/shared/services/UnitTest.service";
import { UnitTest } from "src/app/shared/models/UnitTest.model";
import * as pbi from 'powerbi-client';
import { ElementRef } from "@angular/core";
import notify from "devextreme/ui/notify";
import { Structur } from "src/app/shared/models/Structur.model";
import { HttpClient } from "@angular/common/http";

@Component({
  selector: "app-utExecute",
  templateUrl: "./utExecute.component.html",
  //styleUrls: ["./utExecute.component.css"],
})
export class utExecuteComponent{
  dataSourceUnitTest: DataSource;
  currentSelectedUnitTest: UnitTest;
  Ergebnis: string;
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  


  constructor(
    private UnitTestService: UnitTestService,
    private layoutService: LayoutService,
  ) {
    this.dataSourceUnitTest = new DataSource({
      store: this.UnitTestService.getStore(),
      
    });

  }
  

  public executeUnitTest(){
    let UnitTestData: Structur[] = this.dataGrid.instance.getSelectedRowsData();
    

    //this.UnitTestService.executeUnitTest(UnitTestData[0]).subscribe(data => this.Ergebnis = data);  
    
    if (UnitTestData.length == 0){
        return;
      }

      this.UnitTestService.executeUnitTest(UnitTestData[0]);
      
    //this.Ergebnis = this.UnitTestService.executeUnitTest(UnitTestData[0])
  }
  public LoadWorkspace(event){
    let UnitTestData: UnitTest[] = this.dataGrid.instance.getSelectedRowsData();

    this.UnitTestService.LoadWorkspace();
    notify("Workspaces werden geladen", "success", 6000);
  }

  public LoadDataset(event){
    let UnitTestData: UnitTest[] = this.dataGrid.instance.getSelectedRowsData();

    this.UnitTestService.LoadDataset();
    notify("Datasets werden geladen", "success", 6000);
  }

}