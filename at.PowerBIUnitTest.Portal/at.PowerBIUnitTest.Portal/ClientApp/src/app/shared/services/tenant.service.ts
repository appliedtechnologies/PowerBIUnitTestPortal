import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ODataStore from "devextreme/data/odata/store";
import { AppConfig } from "../config/app.config";
import { ODataService } from "./odata.service";
import { UserService } from "./user.service";

@Injectable({
  providedIn: "root",
})
export class TenantService {

  constructor(
    private odataService: ODataService,
    private userService: UserService,
    private http: HttpClient
  ) {}

  getStore(): ODataStore {
    return this.odataService.context["Tenants"];
  }
}
