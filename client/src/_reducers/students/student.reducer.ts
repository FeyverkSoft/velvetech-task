import { StudentActionsType } from "../../_actions";
import { IAction, IHolded } from "../../core";
import clonedeep from 'lodash.clonedeep';
import { IStudent } from "../../_services/students/GuildInfo";

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

export function students(state: StudentStore = new StudentStore(), action: IAction<StudentActionsType>): StudentStore {
    var clonedState = clonedeep(state);
    switch (action.type) {

        case StudentActionsType.PROC_GET_LIST:
            clonedState.holding = true;
            return clonedState;
        case StudentActionsType.SUCC_GET_LIST:
            clonedState.students = action.students.items;
            clonedState.holding = false;
            return clonedState;
        case StudentActionsType.FAILED_GET_LIST:
            clonedState.holding = false;
            return clonedState;

        case StudentActionsType.SUCC_ADD:
            clonedState.newUserHolding = false;
            return clonedState;
        case StudentActionsType.PROC_ADD:
            clonedState.newUserHolding = true;
            return clonedState;
        case StudentActionsType.FAILED_ADD:
            clonedState.newUserHolding = false;
            return clonedState;

        case StudentActionsType.SUCC_UPDATE:
            clonedState.holding = false;
            for (let i = 0; i < clonedState.students.length; i++) {
                if (clonedState.students[i].id === action.student.id) {
                    clonedState.students[i] = action.student;
                    break;
                }
            }
            return clonedState;
        case StudentActionsType.PROC_UPDATE:
            clonedState.holding = true;
            return clonedState;
        case StudentActionsType.FAILED_UPDATE:
            clonedState.holding = false;
            return clonedState;

        case StudentActionsType.PROC_DELETE:
            clonedState.holding = true;
            return clonedState;
        case StudentActionsType.SUCC_DELETE:
            clonedState.total--;
            clonedState.students = clonedState.students.filter(_ => _.id !== action.id)
            clonedState.holding = false;
            return clonedState;
        case StudentActionsType.FAILED_DELETE:
            clonedState.holding = false;
            return clonedState;


        default:
            return state
    }
}