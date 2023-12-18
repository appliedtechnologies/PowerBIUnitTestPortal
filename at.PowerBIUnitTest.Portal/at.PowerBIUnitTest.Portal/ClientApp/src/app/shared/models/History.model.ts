import { TestRuns } from "./TestRuns.model";
import { UnitTest } from "./UnitTest.model";

export class History 
{
    public Id?: number;
    public TimeStamp?: string;
    public Result?: string;
    public LastRun?: string;
    public ExpectedRun: string; 
    public UnitTest: number;

    public UnitTestNavigation?: UnitTest;
    public TestRunNavigation?: TestRuns;
    
}


  