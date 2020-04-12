import React from 'react';
import { connect } from 'react-redux';
import { Form, Input, Button } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';
import { IStore } from '../../_helpers';
import { authService } from '../../_services';

interface UserFormProps {
    isLoading: boolean,
    LogIn(username: string, password: string): void;
}

class _LoginForm extends React.Component<UserFormProps, any> {
    handleSubmit = (values: any) => {
        this.props.LogIn(values.username, values.password);
    };

    render() {
        const { isLoading } = this.props;
        return (
            <Form
                onFinish={this.handleSubmit}
            >
                <Form.Item
                    name="username"
                    rules={[
                        {
                            required: true,
                            message: 'Please input your username!',
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
                            message: 'Please input your username!',
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
                        loading={isLoading}
                    >
                       Login
                    </Button>
                </Form.Item>
            </Form>
        );
    }
}

const connectedLoginForm = connect<{}, {}, any, IStore>(
    (state: IStore) => {
        const { session } = state;
        return {
            isLoading: session && session.holding,
        };
    },
    (dispatch: Function) => {
        return {
            LogIn: (username: string, password: string) => {
                authService.logIn(username,password );
            },
        }
    })(_LoginForm);

export { connectedLoginForm as LoginForm };
