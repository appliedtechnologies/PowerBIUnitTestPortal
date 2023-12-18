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
import { Structur } from "../models/Structur.model";
import {OdataStringResponse} from "../models/odatastringresponse";
import notify from "devextreme/ui/notify";
import { UnitTestExecutionResult } from "../models/UnitTestExecutionResult";

@Injectable()
export class UnitTestService {
  respons: boolean;
  wasUnitTestSuccessful: boolean = false;
  constructor(
    private odataService: ODataService,
    //private UnitTestService: UnitTestService,
    private http: HttpClient,
    

  ) { }

  getStore(): ODataStore {
    return this.odataService.context["UnitTests"];
  }
  //executeUnitTest(unitTestToExecute: UnitTest) {

    //var urlToCall = AppConfig.settings.api.url + "/UnitTests/Execute";

   // this.http.post(urlToCall, unitTestToExecute)
     // .subscribe(
       // result => {
         // console.log(result);
  //},error => {console.log(error)}
        //)
      
  //} 

// Structur / UnitTest
 executeUnitTest(unitTestToExecute: Structur): Promise<void>{
  return new Promise<void>((resolve)=> {

  let request = this.http
        .post<UnitTestExecutionResult>(`${AppConfig.settings.api.url}/UnitTests/Execute`, unitTestToExecute)
        .subscribe(
          response => {

            resolve();
            if(response.unitTestExecuted == true){

              if(response.unitTestSucceeded == true){
                 /* e.rowElement.style.backgroundcolor = "green";*/
                //notify("UnitTest: " + unitTestToExecute.Name + " war erfolgreich!" + " Erwartetes Ergebnis: " + unitTestToExecute.ExpectedResult + " Letztes Ergebnis: " + unitTestToExecute.LastResult, "success", 6000);
                this.wasUnitTestSuccessful = true;
              }
              else{
                /*e.rowElement.style.backgroundcolor = "red";
                return response.unitTestSucceeded*/
                //notify("UnitTest: " + unitTestToExecute.Name + " nicht bestanden!" + " Erwartetes Ergebnis: " + unitTestToExecute.ExpectedResult + " Letztes Ergebnis: " + unitTestToExecute.LastResult, "info", 6000);
              }
            }
            else{
              notify("UnitTest: " + unitTestToExecute.Name + " konnte nicht ausgeführt werden", "error", 3000);
            }

          },
          error => {
            notify(error.message, "error", 3000);
          }
          
          );
           
        });
       
        //return this.odataService.context["UnitTests/Execute"];
 }

 LoadWorkspace(){
  let request = this.http
        .post(`${AppConfig.settings.api.url}/UnitTests/LoadWorkspace`, null)
        .subscribe(() => {console.log("OK")});
 }

 LoadDataset(){
  let request = this.http
        .post(`${AppConfig.settings.api.url}/UnitTests/LoadDataset`, null)
        .subscribe(() => {console.log("OK")});
 }

 SaveTestRun(result : string, type : string, count : number, Name : String){
  let body = {
    result : result,
    type : type,
    count : count
  }

  let request = this.http
        .post(`${AppConfig.settings.api.url}/UnitTests/SaveTestRun`, {
          Result : result, Type : type, Count : count, Name: Name,
        })
        .subscribe(() => {console.log("OK")});
 }

 
}
