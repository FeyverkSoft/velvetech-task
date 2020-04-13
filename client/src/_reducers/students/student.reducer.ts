import { StudentActionsTypes } from "../../_actions";
import { IHolded } from "../../core";
import clonedeep from 'lodash.clonedeep';
import { IStudent } from "../../_services/students/IStudent";

export class StudentStore {
    total: number;
    offset: number;
    limit: number;
    students: Array<IStudent & IHolded>;
    holding: boolean;
    newUserHolding: boolean = false;

    constructor() {
        this.students = [];
        this.total = 0;
        this.offset = 0;
        this.limit = 20;
        this.holding = false;
    }
}

export function students(state: StudentStore = new StudentStore(), action: StudentActionsTypes): StudentStore {
    var clonedState = clonedeep(state);
    switch (action.type) {

        case 'PROC_GET_STUDENTS':
            clonedState.holding = true;
            return clonedState;
        case 'SUCC_GET_STUDENTS':
            clonedState.students = action.students.items;
            clonedState.holding = false;
            return clonedState;
        case 'FAILED_GET_STUDENTS':
            clonedState.holding = false;
            return clonedState;

        case 'SUCC_ADD_STUDENT':
            clonedState.newUserHolding = false;
            return clonedState;
        case 'PROC_ADD_STUDENT':
            clonedState.newUserHolding = true;
            return clonedState;
        case 'FAILED_ADD_STUDENT':
            clonedState.newUserHolding = false;
            return clonedState;

        case 'SUCC_UPDATE_STUDENT':
            clonedState.holding = false;
            for (let i = 0; i < clonedState.students.length; i++) {
                if (clonedState.students[i].id === action.student.id) {
                    clonedState.students[i] = action.student;
                    break;
                }
            }
            return clonedState;
        case 'PROC_UPDATE_STUDENT':
            clonedState.holding = true;
            return clonedState;
        case 'FAILED_UPDATE_STUDENT':
            clonedState.holding = false;
            return clonedState;

        case 'PROC_DELETE_STUDENT':
            clonedState.holding = true;
            return clonedState;
        case 'SUCC_DELETE_STUDENT':
            clonedState.total--;
            clonedState.students = clonedState.students.filter(_ => _.id !== action.id)
            clonedState.holding = false;
            return clonedState;
        case 'FAILED_DELETE_STUDENT':
            clonedState.holding = false;
            return clonedState;


        default:
            return state
    }
}