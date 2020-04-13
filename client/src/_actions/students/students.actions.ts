import { studentService } from '../../_services';
import { StudentActionsType } from './StudentActionsType';
import { alertInstance } from '..';
import { Gender, Student } from '../../_services/students/IStudent';

export class StudentsActions {
    addStudent(params: { id: string; publicId: string; firstName: string; lastName: string; secondName: string; gender: Gender; }): Function {
        return (dispatch: Function) => {
            dispatch(StudentActionsType.PROC_ADD_STUDENT());
            studentService.addNewUser(
                params.id,
                params.publicId,
                params.firstName,
                params.lastName,
                params.secondName,
                params.gender
            )
                .then(
                    data => {
                        dispatch(StudentActionsType.SUCC_ADD_STUDENT());
                        dispatch(this.getStudentList({ offset: 0, limit: 20 }))
                    })
                .catch(
                    ex => {
                        dispatch(alertInstance.error(ex));
                        dispatch(StudentActionsType.FAILED_ADD_STUDENT());
                    });
        }
    }

    updateStudent(params: { id: string; publicId: string; firstName: string; lastName: string; secondName: string; gender: Gender; }): Function {
        return (dispatch: Function) => {
            dispatch(StudentActionsType.PROC_UPDATE_STUDENT());
            studentService.updateStudent(
                params.id,
                params.publicId,
                params.firstName,
                params.lastName,
                params.secondName,
                params.gender
            )
                .then(
                    data => {
                        dispatch(StudentActionsType.SUCC_UPDATE_STUDENT(new Student(params.id,
                            params.publicId,
                            params.firstName,
                            params.lastName,
                            params.secondName,
                            params.gender)));
                    })
                .catch(
                    ex => {
                        dispatch(alertInstance.error(ex));
                        dispatch(StudentActionsType.FAILED_UPDATE_STUDENT());
                    });
        }

    }

    getStudentList(params: {
        offset: number,
        limit: number,
        filter?: string
    }): Function {
        return (dispatch: Function) => {
            dispatch(StudentActionsType.PROC_GET_STUDENTS());
            studentService.getStudentList(params.offset, params.limit, params.filter)
                .then(
                    data => {
                        dispatch(StudentActionsType.SUCC_GET_STUDENTS(data));
                    })
                .catch(
                    ex => {
                        dispatch(StudentActionsType.FAILED_GET_STUDENTS());
                        dispatch(alertInstance.error(ex));
                    });
        }
    }

    deleteUser(id: string): Function {
        return (dispatch: Function) => {
            dispatch(StudentActionsType.PROC_DELETE_STUDENT());
            studentService.deleteUser(id)
                .then(
                    data => {
                        dispatch(StudentActionsType.SUCC_DELETE_STUDENT(id));
                    })
                .catch(
                    ex => {
                        dispatch(alertInstance.error(ex));
                        dispatch(StudentActionsType.FAILED_DELETE_STUDENT());
                    });
        }
    }
}

export const studentsInstance = new StudentsActions();