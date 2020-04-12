import React from 'react';
import { connect } from 'react-redux';
import { Form, Input, Button } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';
import { IStore } from '../../_helpers';
import { authService } from '../../_services';

interface UserFormProps {
    Reg(username: string, login: string, password: string): void;
}

class _RegForm extends React.Component<UserFormProps, any> {
    handleSubmit = (values: any) => {
        this.props.Reg(values.name, values.login, values.password);
    };

    render() {
        return (
            <Form
                onFinish={this.handleSubmit}
            >
                <Form.Item
                    name="login"
                    rules={[
                        {
                            required: true,
                            message: 'Please input your login!',
                        },
                    ]}
                >
                    <Input
                        prefix={<UserOutlined />}
                        placeholder='login'
                    />
                </Form.Item>
                <Form.Item
                    name="name"
                    rules={[
                        {
                            required: true,
                            message: 'Please input your name!',
                        },
                    ]}
                >
                    <Input
                        prefix={<UserOutlined />}
                        placeholder='Name'
                    />
                </Form.Item>
                <Form.Item
                    name="password"
                    rules={[
                        {
                            required: true,
                            message: 'Please input your password!',
                        }]}
                >

                    <Input
                        prefix={<LockOutlined />}
                        type="password"
                        placeholder='Password'
                    />
                </Form.Item>
                <Form.Item>
                    <Button
                        type="primary"
                        htmlType="submit"
                    >
                        Reg
                    </Button>
                </Form.Item>
            </Form>
        );
    }
}

const connectedRegForm = connect<{}, {}, any, IStore>(
    (state: IStore) => {
        return {};
    },
    (dispatch: Function) => {
        return {
            Reg: (username: string, login: string, password: string) => {
                authService.Reg(username, login, password);
            },
        }
    })(_RegForm);

export { connectedRegForm as RegForm };
