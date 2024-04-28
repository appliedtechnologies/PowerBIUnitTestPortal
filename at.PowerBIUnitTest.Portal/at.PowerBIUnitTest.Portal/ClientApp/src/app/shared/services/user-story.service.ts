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
import { UserStory } from "../models/user-story.model";
import { CrudBaseService } from "./crud-base.service";
@Injectable()
export class UserStoryService extends CrudBaseService<UserStory>{

  constructor(
    private odataService: ODataService,
    private http: HttpClient,
  ) {
    super();
  }

  public getStore(): ODataStore {
    return this.odataService.context["UserStories"];
  }

  public copyToOtherTabularModel(userStoryId: number, targetTabularModelId: number): Promise<void> {
    return new Promise<void>((resolve, reject) => {
      let request = this.http
        .post(`${AppConfig.settings.api.url}/UserStories(${userStoryId})/Copy`, {
          targetTabularModelId: targetTabularModelId
        })
        .subscribe({
          next: (data) => resolve(),
          error: (error) => reject(error?.error?.error),
        });
    });
  }
}