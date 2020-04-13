import { PagedStudents, Student } from "../../_services/students/IStudent"

export namespace StudentActionsType {
    export const PROC_GET_STUDENTS = () => ({
        type: "PROC_GET_STUDENTS",
    } as const)
    export const SUCC_GET_STUDENTS = (students: PagedStudents) => ({
        type: "SUCC_GET_STUDENTS",
        students: students,
    } as const)
    export const FAILED_GET_STUDENTS = () => ({
        type: "FAILED_GET_STUDENTS",
    } as const)


    export const SUCC_ADD_STUDENT = () => ({
        type: "SUCC_ADD_STUDENT",
    } as const)
    export const PROC_ADD_STUDENT = () => ({
        type: "PROC_ADD_STUDENT",
    } as const)
    export const FAILED_ADD_STUDENT = () => ({
        type: "FAILED_ADD_STUDENT",
    } as const)


    export const PROC_UPDATE_STUDENT = () => ({
        type: "PROC_UPDATE_STUDENT",
    } as const)
    export const SUCC_UPDATE_STUDENT = (student: Student) => ({
        type: "SUCC_UPDATE_STUDENT",
        student: student,
    } as const)
    export const FAILED_UPDATE_STUDENT = () => ({
        type: "FAILED_UPDATE_STUDENT",
    } as const)


    export const PROC_DELETE_STUDENT = () => ({
        type: "PROC_DELETE_STUDENT",
    } as const)
    export const SUCC_DELETE_STUDENT = (id: string) => ({
        type: "SUCC_DELETE_STUDENT",
        id: id,
    } as const)
    export const FAILED_DELETE_STUDENT = () => ({
        type: "FAILED_DELETE_STUDENT",
    } as const)
}

export type StudentActionsTypes =
    ReturnType<typeof StudentActionsType.PROC_GET_STUDENTS> |
    ReturnType<typeof StudentActionsType.SUCC_GET_STUDENTS> |
    ReturnType<typeof StudentActionsType.FAILED_GET_STUDENTS> |

    ReturnType<typeof StudentActionsType.SUCC_ADD_STUDENT> |
    ReturnType<typeof StudentActionsType.PROC_ADD_STUDENT> |
    ReturnType<typeof StudentActionsType.FAILED_ADD_STUDENT> |

    ReturnType<typeof StudentActionsType.PROC_UPDATE_STUDENT> |
    ReturnType<typeof StudentActionsType.SUCC_UPDATE_STUDENT> |
    ReturnType<typeof StudentActionsType.FAILED_UPDATE_STUDENT> |

    ReturnType<typeof StudentActionsType.PROC_DELETE_STUDENT> |
    ReturnType<typeof StudentActionsType.SUCC_DELETE_STUDENT> |
    ReturnType<typeof StudentActionsType.FAILED_DELETE_STUDENT>;