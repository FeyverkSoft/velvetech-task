import { studentService } from '../../_services';
import { StudentActionsType } from './StudentActionsType';
import { alertInstance } from '..';
import { PagedStudents, Gender, Student } from '../../_services/students/GuildInfo';

export class StudentsActions {
    addStudent(params: { id: string; publicId: string; firstName: string; lastName: string; secondName: string; gender: Gender; }): Function {
        return (dispatch: Function) => {
            dispatch(request());
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
                        dispatch(success());
                        dispatch(this.getStudentList({ offset: 0, limit: 20 }))
                    })
                .catch(
                    ex => {
                        dispatch(alertInstance.error(ex));
                        dispatch(failure());
                    });
        }
        function request() { return { type: StudentActionsType.PROC_ADD } }
        function success() { return { type: StudentActionsType.SUCC_ADD } }
        function failure() { return { type: StudentActionsType.FAILED_ADD } }
    }

    updateStudent(params: { id: string; publicId: string; firstName: string; lastName: string; secondName: string; gender: Gender; }): Function {
        return (dispatch: Function) => {
            dispatch(request());
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
                        dispatch(success(new Student(params.id,
                            params.publicId,
                            params.firstName,
                            params.lastName,
                            params.secondName,
                            params.gender)));
                    })
                .catch(
                    ex => {
                        dispatch(alertInstance.error(ex));
                        dispatch(failure());
                    });
        }
        function request() { return { type: StudentActionsType.PROC_UPDATE } }
        function success(student: Student) { return { type: StudentActionsType.SUCC_UPDATE, student } }
        function failure() { return { type: StudentActionsType.FAILED_UPDATE } }
    }

    getStudentList(params: {
        offset: number,
        limit: number,
        filter?: string
    }): Function {
        return (dispatch: Function) => {
            dispatch(request());
            studentService.getStudentList(params.offset, params.limit, params.filter)
                .then(
                    data => {
                        dispatch(success(data));
                    })
                .catch(
                    ex => {
                        dispatch(failure());
                        dispatch(alertInstance.error(ex));
                    });
        }
        function request() { return { type: StudentActionsType.PROC_GET_LIST } }
        function success(students: PagedStudents) { return { type: StudentActionsType.SUCC_GET_LIST, students } }
        function failure() { return { type: StudentActionsType.FAILED_GET_LIST } }
    }

    deleteUser(id: string): Function {
        return (dispatch: Function) => {
            dispatch(request());
            studentService.deleteUser(id)
                .then(
                    data => {
                        dispatch(success(id));
                    })
                .catch(
                    ex => {
                        dispatch(alertInstance.error(ex));
                        dispatch(failure());
                    });
        }
        function request() { return { type: StudentActionsType.PROC_DELETE } }
        function success(id: string) { return { type: StudentActionsType.SUCC_DELETE, id } }
        function failure() { return { type: StudentActionsType.FAILED_DELETE } }
    }
}

export const studentsInstance = new StudentsActions();