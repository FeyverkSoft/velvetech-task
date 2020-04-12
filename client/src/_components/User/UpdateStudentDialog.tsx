import React from 'react';
import { Form, Input, Modal, Select } from 'antd';
import { IStore } from '../../_helpers';
import { Gender, genderList, IStudent } from '../../_services/students/GuildInfo';
import { connect } from 'react-redux';

interface FormProps {
    isLoading?: boolean,
    visible: boolean;
    student: IStudent;
    onClose(): void;
    onUpdate(studentId: string,
        publicId: string,
        firstName: string,
        lastName: string,
        secondName: string,
        gender: Gender
    ): void;
}

const updateStudentDialog = ({ ...props }: FormProps) => {
    const { isLoading, visible, student } = props;
    const [form] = Form.useForm();;
    form.setFieldsValue(student);
    return (
        <Modal
            title='Add new student'
            key={student.id}
            visible={visible}
            onCancel={() => {
                props.onClose();
                form.resetFields();
            }}
            okText='Add'
            confirmLoading={isLoading}
            onOk={() => {
                form.validateFields()
                    .then(values => {
                        props.onUpdate(student.id,
                            values.publicId,
                            values.firstName,
                            values.lastName,
                            values.secondName,
                            values.gender
                        );
                        props.onClose();
                        form.resetFields();
                    })
                    .catch(info => {
                        console.log('Validate Failed:', info);
                    });
            }}
        >
            <Form
                form={form}
                layout='vertical'
                key={student.id}
            >
                <Form.Item
                    name="publicId"
                    label='PublicId'
                    rules={[
                        {
                            required: false,
                            max: 16,
                            min: 6,
                            message: 'Lenght beetwen 6 and 16',
                        },
                    ]}
                >
                    <Input
                        placeholder='PublicId'
                    />
                </Form.Item>
                <Form.Item
                    name="firstName"
                    label='First name'
                    rules={[
                        {
                            required: true,
                            message: 'Please input firstName!',
                        }]}
                >
                    <Input
                        placeholder='First name'
                    />
                </Form.Item>
                <Form.Item
                    name="lastName"
                    label='Last name'
                    rules={[
                        {
                            required: true,
                            message: 'Please input lastName!',
                        }]}
                >
                    <Input
                        placeholder='Last name'
                    />
                </Form.Item>
                <Form.Item
                    name="secondName"
                    label='Seconf name'
                    rules={[
                        {
                            required: true,
                            message: 'Please input secondName!',
                        }]}
                >
                    <Input
                        placeholder='Second name'
                    />
                </Form.Item>
                <Form.Item
                    name="gender"
                    label="Gender"
                    rules={[
                        {
                            required: true,
                            message: 'Please input gender!',
                        }]}
                >
                    <Select
                    >
                        {
                            genderList.map(t => <Select.Option key={t} value={t}>{t}</Select.Option>)
                        }
                    </Select>
                </Form.Item>
            </Form>
        </Modal>
    );
}

const connectedUpdateStudentDialog = connect<{}, {}, any, IStore>(
    (store: IStore, pros: any) => {
        const student = store.students.students.filter(_ => _.id === pros.studentId)[0];
        return {
            student: student || {}
        }
    },
)(updateStudentDialog);

export { connectedUpdateStudentDialog as UpdateStudentDialog };