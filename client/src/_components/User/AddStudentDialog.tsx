import React from 'react';
import { Form, Input, Modal, Select } from 'antd';
import { getGuid } from '../../_helpers';
import { Gender, genderList } from '../../_services/students/IStudent';

interface FormProps {
    isLoading?: boolean,
    visible: boolean;
    onClose(): void;
    onAdd(studentId: string,
        publicId: string,
        firstName: string,
        lastName: string,
        secondName: string,
        gender: Gender
    ): void;
}

export const AddStudentDialog = ({ ...props }: FormProps) => {
    const { isLoading, visible } = props;
    const [form] = Form.useForm();
    const id = getGuid();
    return (
        <Modal
            title='Add new student'
            visible={visible}
            onCancel={() => props.onClose()}
            okText='Add'
            confirmLoading={isLoading}
            onOk={() => {
                form.validateFields()
                    .then(values => {
                        form.resetFields();
                        props.onAdd(id,
                            values.publicId,
                            values.firstName,
                            values.lastName,
                            values.secondName,
                            values.gender
                        );
                        props.onClose();
                    })
                    .catch(info => {
                        console.log('Validate Failed:', info);
                    });
            }}
        >
            <Form
                form={form}
                layout='vertical'
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
                    <Select>
                        {
                            genderList.map(t => <Select.Option key={t} value={t}>{t}</Select.Option>)
                        }
                    </Select>
                </Form.Item>
            </Form>
        </Modal>
    );
}