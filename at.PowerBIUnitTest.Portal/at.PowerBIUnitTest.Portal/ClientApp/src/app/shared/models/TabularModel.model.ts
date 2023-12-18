import { UserStory } from "./UserStory.model";
import { Workspace } from "./workspace.model";

export interface TabularModel {
    Id?: number;
    DatasetPbId?: string;
    Workspace?: number;
    Name?: string;

    WorkspaceNavigation?: Workspace;
    UserStories?: UserStory[];
}