import { Config } from '../../core';
import { PagedStudents } from './GuildInfo';
import axios from 'axios';

export class studentService {
    static async deleteUser(id: string) {
        await axios.delete(Config.BuildUrl(`/students/${id}`))
    };

    static async addNewUser(id: string, publicId: string, firstName: string, lastName: string, secondName: string, gender: string): Promise<void> {
        await axios.post(Config.BuildUrl(`/students`), {
            id,
            publicId,
            firstName,
            lastName,
            secondName,
            gender,
        });
    }

    static async updateStudent(id: string, publicId: string, firstName: string, lastName: string, secondName: string, gender: string): Promise<void> {
        await axios.post(Config.BuildUrl(`/students/${id}`), {
            publicId,
            firstName,
            lastName,
            secondName,
            gender,
        });
    }

    static async getStudentList(offset: number, limit: number, filter?: string):
        Promise<PagedStudents> {
        return await axios.get(Config.BuildUrl(`/students`, {
            offset: offset,
            limit: limit,
            filter: filter
        }))
            .then(_ => _.data);
    }
}
