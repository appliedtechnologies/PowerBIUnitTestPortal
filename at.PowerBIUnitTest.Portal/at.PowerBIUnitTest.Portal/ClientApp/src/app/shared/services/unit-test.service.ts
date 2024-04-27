import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ODataStore from "devextreme/data/odata/store";
import { AppConfig } from "../config/app.config";
import { ODataService } from "./odata.service";
import { UserService } from "./user.service";
import { LayoutService } from "./layout.service";
import { UnitTest } from "src/app/shared/models/unit-test.model";
import ODataContext from "devextreme/data/odata/context";
import { map } from "rxjs/operators";
import notify from "devextreme/ui/notify";

@Injectable()
export class UnitTestService {
  constructor(
    private odataService: ODataService,
    private http: HttpClient
  ) {}

  getStore(): ODataStore {
    return this.odataService.context["UnitTests"];
  }

  public remove (id: number): Promise<void> {
    return this.getStore().remove(id);
  }
}
