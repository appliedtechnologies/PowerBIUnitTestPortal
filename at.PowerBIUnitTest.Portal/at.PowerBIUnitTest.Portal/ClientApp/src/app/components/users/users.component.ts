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
import { DxListComponent, DxPopupComponent } from "devextreme-angular";

@Component({
  selector: "app-user",
  templateUrl: "./users.component.html",
  styleUrls: ["./users.component.css"],
})
export class UserComponent {
  dataSourceUsers: DataSource;
  currentSelectedUser: User;


  constructor(
    private userService: UserService,
    private layoutService: LayoutService,
  ) {
    this.dataSourceUsers = new DataSource({
      store: this.userService.getStore(),
      expand: "TenantNavigation",
    });
  }
}
