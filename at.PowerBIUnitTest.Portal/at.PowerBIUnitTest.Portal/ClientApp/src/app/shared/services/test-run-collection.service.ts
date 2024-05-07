import { Injectable } from '@angular/core';
import { ODataService } from './odata.service';
import ODataStore from 'devextreme/data/odata/store';

@Injectable()
export class TestRunCollectionService {

    constructor(private odataService: ODataService) {
    }

    getStore(): ODataStore {
        return this.odataService.context["TestRunCollections"];
    }
}