import { User } from "oidc-client";
import { UnitTest } from "./unit-test.model";
import { TestRunCollection } from "./test-run-collection.model";

export interface TestRun {
    Id?: number;
    TimeStamp?: string;
    WasPassed?: boolean;
    Result?: string;
    ExpectedResult?: string;
    UnitTest?: number;
    TestRunCollection?: number;
    CreatedBy?: number | null;
    CreatedOn?: string | null;
    ModifiedBy?: number | null;
    ModifiedOn?: string | null;
    CreatedByNavigation?: User;
    ModifiedByNavigation?: User;
    UnitTestNavigation?: UnitTest;
    TestRunCollectionNavigation?: TestRunCollection;
}