import { UserStory } from "./UserStory.model";
import { History } from "./History.model";
import { ResultType } from "./ResultType";

export class UnitTest 
{
    public Id?: number;
  
    public Name?: string;
    public ExpectedResult?: string;
    public LastResult?: string;
    public Timestamp?: string;
    public UserStory?: number;
    public DAX?: string;
    public ResultType?: string;
    public DateTimeFormat?: string;
    public DecimalPlaces?: string;
    public FloatSeparators?: string;

    UserStoryNavigation?: UserStory;
    ResultTypeNavigation?: ResultType;
    Histories?: History[];
  }
  