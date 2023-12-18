import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ODataStore from "devextreme/data/odata/store";
import { AppConfig } from "../config/app.config";
import { ODataService } from "./odata.service";
import { UserService } from "./user.service";
import { LayoutService } from "./layout.service";
import { UnitTest } from "src/app/shared/models/UnitTest.model";
import ODataContext from "devextreme/data/odata/context";
import { map } from "rxjs/operators";
import { History } from "../models/History.model";
@Injectable()
export class HistoryService {

  constructor(
    private odataService: ODataService,
    //private UserStoryService: UserStoryService,
    private http: HttpClient,

  ) { }

  getStore(): ODataStore {
    return this.odataService.context["History"];
  }

}