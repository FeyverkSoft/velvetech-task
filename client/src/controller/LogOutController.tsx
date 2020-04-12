import * as React from 'react';
import { Card, Button, Breadcrumb } from 'antd';
import { HomeOutlined } from '@ant-design/icons';
import style from './auth.module.scss';
import { connect } from 'react-redux';
import { IStore } from '../_helpers/store';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';
import { authService } from '../_services';

interface LogOutControllerProps {
    isLoading: boolean;
    logOut: Function;
}
const _LogOutController = ({ ...props }: LogOutControllerProps) => {
    return (
        <Content>
            <Breadcrumb>
                <Breadcrumb.Item>
                    <Link to={"/"} >
                        <HomeOutlined />
                    </Link>
                </Breadcrumb.Item>
                <Breadcrumb.Item>
                    <Link to={"/logout"} >
                        Logout page
                    </Link>
                </Breadcrumb.Item>
            </Breadcrumb>
            <div className={style['auth']}>
                <Card
                    title='Logout page'
                    style={{ width: '500px' }}
                >
                    <Button
                        type="primary"
                        htmlType="submit"
                        loading={props.isLoading}
                        onClick={() => props.logOut()}
                    >
                        Logout
                    </Button>
                </Card>
            </div>
        </Content>
    );
}

const connectedLogOutController = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const { session } = state;
        return {
            isLoading: session && session.holding,
        };
    },
    (dispatch: Function) => {
        return {
            logOut: () => {
                authService.logOut();
            },
        }
    })(_LogOutController);

export { connectedLogOutController as LogOutController };
