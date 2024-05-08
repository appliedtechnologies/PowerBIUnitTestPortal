import { Component, OnInit } from '@angular/core';
import DataSource from 'devextreme/data/data_source';
import { TestRunCollectionService } from 'src/app/shared/services/test-run-collection.service';

@Component({
    selector: 'app-history',
    templateUrl: './history.component.html',
    styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
    public dataSourceTestRunCollections: DataSource;

    constructor(private testRunCollectionService: TestRunCollectionService) {
        this.dataSourceTestRunCollections = new DataSource({
            store: testRunCollectionService.getStore(),
            expand: ["TestRuns"],
            sort: [{ selector: "TimeStamp", desc: false }]
        });
     }

    ngOnInit(): void {
        // Add initialization logic here
    }
}