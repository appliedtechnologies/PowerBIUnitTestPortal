import { User } from "oidc-client";
import { TestRun } from "./test-run.model";

export interface TestRunCollection {
    Id?: number;
    TimeStamp?: string;
    WasPassed?: string;
    CreatedBy?: number | null;
    CreatedOn?: string | null;
    ModifiedBy?: number | null;
    ModifiedOn?: string | null;
    CreatedByNavigation?: User;
    ModifiedByNavigation?: User;
    TestRuns?: TestRun[];
}