import { UnitTest } from "./unit-test.model";

export interface ResultType {
    Id?: number;
    Name?: string;
    UnitTests?: UnitTest[];
}