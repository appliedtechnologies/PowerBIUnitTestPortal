import { Component, Input, OnInit } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';
import { TestRunCollectionService } from 'src/app/shared/services/test-run-collection.service';

@Component({
    selector: 'app-history',
    templateUrl: './history.component.html',
    styleUrls: ['./history.component.css']
})
export class HistoryComponent {
    @Input() unitTestId:number;

    public dataSourceTestRunCollections: DataSource;

    constructor(private testRunCollectionService: TestRunCollectionService) {
        this.dataSourceTestRunCollections = new DataSource({
            store: new CustomStore({
                key: "UniqueIdentifier",
                load: async (loadOptions) => {
                    if (loadOptions.expand == null)
                        loadOptions.expand = new Array();
                    loadOptions.expand.push("TestRuns");
                    loadOptions.expand.push("TestRuns.UnitTestNavigation($select=Name)");

                    loadOptions.sort = { selector: "TimeStamp", desc: true }; 

                    if(this.unitTestId != null){
                        loadOptions.filter = ["TestRuns.any(d:d.UnitTest eq " + this.unitTestId + ")"];
                    }

                    return (this.testRunCollectionService.getStore().load(loadOptions)).then((data) => {
                        data.forEach(e => {
                            delete Object.assign(e, { ["items"]: e["TestRuns"] })["TestRuns"]
                            e["type"] = "Collection";
                            e["parentId"] = 0;
                            e["items"]?.forEach(ee => {
                                ee["type"] = "Test Run";
                                ee["parentId"] = e["UniqueIdentifier"];
                            })
                        });
                    });
                }
            }),
        });
     }

    public onCellPreparedTreeList(e: any): void {
        if (e.rowType === "data" && e.row.data.type == "Test Run" && e.column.dataField === "Result") {
            e.cellElement.style.color = e.row.data?.WasPassed ? "green" : "red";
        }
    }
}