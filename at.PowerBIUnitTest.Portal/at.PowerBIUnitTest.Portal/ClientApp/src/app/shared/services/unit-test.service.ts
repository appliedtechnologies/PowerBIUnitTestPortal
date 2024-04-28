import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import ODataStore from "devextreme/data/odata/store";
import { AppConfig } from "../config/app.config";
import { ODataService } from "./odata.service";
import { UserService } from "./user.service";
import { LayoutService } from "./layout.service";
import { UnitTest } from "src/app/shared/models/unit-test.model";
import { CrudBaseService } from "src/app/shared/services/crud-base.service";
import ODataContext from "devextreme/data/odata/context";
import { map } from "rxjs/operators";
import notify from "devextreme/ui/notify";

@Injectable()
export class UnitTestService extends CrudBaseService<UnitTest> {
  constructor(
    private odataService: ODataService,
    private http: HttpClient
  ) {
    super();
  }

  getStore(): ODataStore {
    return this.odataService.context["UnitTests"];
  }

  public executeMultiple(ids: number[]): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      let request = this.http
        .post(`${AppConfig.settings.api.url}/UnitTests/Execute`, { unitTestIds: ids })
        .subscribe({
          next: () => resolve(),
          error: (error: Error) => reject(error["error"]["error"])
        });
    });
  }
}
