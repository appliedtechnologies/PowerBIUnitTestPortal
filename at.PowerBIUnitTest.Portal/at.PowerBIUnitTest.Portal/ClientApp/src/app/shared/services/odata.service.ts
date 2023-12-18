import { Injectable } from "@angular/core";
import { LogService } from "./log.service";
import { AppConfig } from "../config/app.config";
import { Router } from "@angular/router";
import { UserService } from "./user.service";
import ODataContext from "devextreme/data/odata/context";

@Injectable()
export class ODataService {
  context: ODataContext;
  constructor(private router: Router, private logService: LogService) {
    this.context = new ODataContext({
      beforeSend: (e) => {},
      url: AppConfig.settings.api.url,
      version: 4,
      errorHandler: (error) => {
        this.logService.error(
          error.errorDetails,
          "error occurred while odata request"
        );
      },
      entities: {
        Tenants: {
          key: "Id",
          keyType: "Int32",
        },
        Users: {
          key: "Id",
          keyType: "Int32",
        },
        Workspaces: {
          key: "Id",
          keyType: "Int32",
        },
        TabularModels:{
          key: "Id",
          keyType: "Int32",
        },
        UserStories: {
          key: "Id",
          keyType: "Int32",
        }, 
        UnitTests: {
          key: "Id",
          keyType: "Int32",
        },
        
      },
    });
  }

  public useDate(date: Date): Date {
    return new Date(date.toUTCString().substr(0, 25));
  }
}
