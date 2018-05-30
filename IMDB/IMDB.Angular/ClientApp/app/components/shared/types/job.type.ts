import { PersonJobs } from "./person-jobs.type";

export class Job {
    jobId: number;
    description: string;
    name: string;
    personJobs: PersonJobs[];
    constructor() {
    }
}