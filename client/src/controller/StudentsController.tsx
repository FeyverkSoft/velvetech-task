import * as React from 'react';
import { Breadcrumb, PageHeader, Row, Col, Button, Tooltip } from 'antd';
import { DeleteFilled } from '@ant-design/icons';
import { HomeOutlined } from '@ant-design/icons';
import style from './auth.module.scss';
import { Content } from '../_components/Content/Content';
import { Link } from 'react-router-dom';
import { IStudent, Gender } from '../_services/students/IStudent';
import Table, { ColumnProps } from 'antd/lib/table';
import { IStore } from '../_helpers';
import { studentsInstance } from '../_actions';
import { connect } from 'react-redux';
import { PaginationConfig } from 'antd/lib/pagination';
import Search from 'antd/lib/input/Search';
import { AddStudentDialog } from '../_components/User/AddStudentDialog';
import { UpdateStudentDialog } from '../_components/User/UpdateStudentDialog';

interface IState {
    filter: string;
    columns: ColumnProps<IStudent>[];
    addStudentModal: ModalState;
    updateStudentModal: ModalState & { id?: string };
}
interface ModalState {
    show: boolean;
}
interface IStudentsProps {
    limit: number;
    offser: number;
    total: number;
    items: Array<IStudent>;
    isLoading: boolean;
    newUserHolding: boolean;
    loadData(limit: number, offset: number, filter?: string): void;
    addUser(id: string, publicId: string, firstName: string, lastName: string, secondName: string, gender: Gender): void;
    updateStudent(id: string, publicId: string, firstName: string, lastName: string, secondName: string, gender: Gender): void;
    delete(id: string): void;
}

export class _StudentsController extends React.Component<IStudentsProps, IState> {
    constructor(props: any) {
        super(props);
        this.state = {
            filter: '',
            addStudentModal: { show: false },
            updateStudentModal: { show: false },
            columns: [
                {
                    title: 'PublicId',
                    dataIndex: 'publicId',
                    key: 'publicId',
                },
                {
                    title: 'First name',
                    dataIndex: 'firstName',
                    key: 'firstName',
                    render: (value: string, record: IStudent) => {
                        return {
                            children: <div >
                                <Tooltip title='edit'>
                                    <Button
                                        type="link"
                                        onClick={() => this.toggleUpdateStudentModal(record.id)}
                                    >
                                        {value}
                                    </Button>
                                </Tooltip>
                            </div>
                        };
                    },
                },
                {
                    title: 'Last name',
                    dataIndex: 'lastName',
                    key: 'lastName',
                },
                {
                    title: 'Second name',
                    dataIndex: 'secondName',
                    key: 'secondName',
                },
                {
                    title: 'Gender',
                    dataIndex: 'gender',
                    key: 'gender',
                },
                {
                    title: 'Actions',
                    dataIndex: 'id',
                    key: 'id',
                    width: 50,
                    fixed: 'right',
                    render: (id: string, record: IStudent) => {
                        return {
                            children: <div >
                                <Tooltip title='Delete'>
                                    <Button
                                        type="link"
                                        icon={<DeleteFilled />}
                                        onClick={() => props.delete(record.id)}
                                    />
                                </Tooltip>
                            </div>
                        };
                    },
                },
            ]
        }
    }

    componentDidMount() {
        this.loadData();
    }

    loadData = (limit: number = 20, offset: number = 0, filter?: string) => {
        this.props.loadData(limit || 20, offset, this.state.filter || filter);
    }

    onChange = (pagination: PaginationConfig) => {
        this.loadData(pagination.pageSize, Number((pagination.pageSize || 20) * ((pagination.current || 1) - 1)), this.state.filter);
    }

    onSearch = (value: string) => {
        this.setState({ filter: (value || '').toLowerCase() }, () => this.loadData());
    }

    toggleAddStudentModal = () => {
        this.setState({ addStudentModal: { show: !this.state.addStudentModal.show } });
    }
    toggleUpdateStudentModal = (id?: string) => {
        this.setState({ updateStudentModal: { show: !this.state.updateStudentModal.show, id } });
    }
    onAddUser = (id: string, publicId: string, firstName: string, lastName: string, secondName: string, gender: Gender) => {
        this.props.addUser(id, publicId, firstName, lastName, secondName, gender);
    };

    render() {
        return (
            <Content>
                <Breadcrumb>
                    <Breadcrumb.Item>
                        <Link to={"/"} >
                            <HomeOutlined />
                        </Link>
                    </Breadcrumb.Item>
                </Breadcrumb>
                <div className={style['bd']}>
                    <PageHeader
                        ghost={false}
                        subTitle=""
                        title='Student list'
                        className={style['ant-card']}
                    >
                        <Row gutter={[16, 16]}>
                            <Col xs={19} sm={19} md={21} lg={22} xl={23}>
                                <Search
                                    placeholder="введите текст для поиска"
                                    enterButton='search'
                                    onSearch={this.onSearch}
                                />
                            </Col>
                            <Col xs={5} sm={5} md={3} lg={2} xl={1}>
                                <Button
                                    onClick={this.toggleAddStudentModal}
                                >
                                    Add
                                </Button>
                            </Col>
                        </Row>
                        <Row gutter={[16, 16]}>
                            <Col xl={24}>
                                {<Table
                                    size='middle'
                                    rowKey="id"
                                    columns={this.state.columns}
                                    pagination={{
                                        pageSize: 20,
                                        total: this.props.total
                                    }}
                                    onChange={this.onChange}
                                    loading={this.props.isLoading}
                                    bordered={false}
                                    dataSource={this.props.items}
                                />}
                            </Col>
                        </Row>
                    </PageHeader>
                    <AddStudentDialog
                        onClose={this.toggleAddStudentModal}
                        visible={this.state.addStudentModal.show}
                        onAdd={this.onAddUser}
                        isLoading={this.props.newUserHolding}
                    />
                    <UpdateStudentDialog
                        onClose={this.toggleUpdateStudentModal}
                        visible={this.state.updateStudentModal.show}
                        onUpdate={this.props.updateStudent}
                        isLoading={this.props.isLoading}
                        studentId={this.state.updateStudentModal.id}
                    />

                </div>
            </Content>
        );
    }
}

const connectedStudentsController = connect<{}, {}, {}, IStore>(
    (state: IStore) => {
        const { total, offset, limit, students, holding } = state.students;
        const { newUserHolding } = state.students;
        return {
            total,
            offset,
            limit,
            items: students,
            isLoading: holding === true,
            newUserHolding
        };
    },
    (dispatch: any) => {
        return {
            loadData: (limit: number, offset: number, filter?: string) => dispatch(studentsInstance.getStudentList({ limit, offset, filter })),
            addUser: (id: string, publicId: string, firstName: string, lastName: string, secondName: string, gender: Gender) => dispatch(
                studentsInstance.addStudent({ id, publicId, firstName, lastName, secondName, gender })
            ),
            delete: (id: string) => dispatch(studentsInstance.deleteUser(id)),
            updateStudent: (id: string, publicId: string, firstName: string, lastName: string, secondName: string, gender: Gender) => dispatch(
                studentsInstance.updateStudent({ id, publicId, firstName, lastName, secondName, gender })
            ),
        }
    })(_StudentsController);

export { connectedStudentsController as StudentsController };