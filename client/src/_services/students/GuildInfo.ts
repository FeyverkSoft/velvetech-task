export type Gender = 'Male' | 'Female' | 'Other';
export const genderList = ['Male' , 'Female' , 'Other'];

export interface IPage<T> {
    total: number,
    offset: number,
    limit: number,
    items: Array<T>;
}

export class PagedStudents implements IPage<Student>{
    total: number;
    offset: number;
    limit: number;
    items: Array<Student>=[];

constructor(total: number,
    offset: number,
    limit: number,
    items: Array<any>) {
    this.total = Number(total);
    this.offset = Number(offset);
    this.limit = Number(limit);
    items.forEach(_=>{
        this.items.push(new Student(
            _.id,
            _.publicId,
            _.firstName,
            _.lastName,
            _.secondName,
            _.gender
        ));
    })
}
}

export interface IStudent {
    id: string;
    publicId: string;
    firstName: string
    lastName: string
    secondName: string
    gender: Gender;
}

export class Student implements IStudent {
    id: string;
    publicId: string;
    firstName: string
    lastName: string
    secondName: string
    gender: Gender;

    constructor(id: string,
        publicId: string,
        firstName: string,
        lastName: string,
        secondName: string,
        gender: Gender) {
        this.id = String(id);
        this.publicId = String(publicId);
        this.firstName = String(firstName);
        this.lastName = String(lastName);
        this.secondName = String(secondName);
        this.gender = gender as Gender;
    }
}