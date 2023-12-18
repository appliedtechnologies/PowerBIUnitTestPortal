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
import { UserStoryService } from "src/app/shared/services/UserStory.service";
import { UserStory } from "src/app/shared/models/UserStory.model";




@Component({
  selector: "app-utCreate",
  templateUrl: "./utCreate.component.html",
  //styleUrls: ["./utExecute.component.css"],
})
export class utCreateComponent{
  dataSourceUserStory: DataSource;
  currentSelectedUnitTest: UserStory;
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;


  constructor(
    private UserStoryService: UserStoryService,
    private layoutService: LayoutService,
  ) {
    this.dataSourceUserStory = new DataSource({
      store: this.UserStoryService.getStore(),
      
    });
    
  }
}

