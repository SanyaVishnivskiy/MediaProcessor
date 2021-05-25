import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import { Router, Route, Link } from "react-router-dom";
import reportWebVitals from './reportWebVitals';
import { Header } from './components/header/header';
import { FileUpload } from './pages/uploads/uploads-page';
import { RecordsPage } from './pages/records/records-page/records-page';
import { RecordEditPage } from './pages/records/record-edit-page/record-edit-page';
import history from './entities/search/history';
import { ActionsPage } from './pages/actions/actions-page';
import { LoginPage } from './pages/auth/login/login-page';
import { Auth } from './services/auth/auth';
import { Redirect } from './services/navigation/redirect';
import { UsersPage } from './pages/auth/users/main/users-page';
import { UserCreatePage } from './pages/auth/users/create/user-create-page';
import { UserEditPage } from './pages/auth/users/edit/user-edit-page';

const isAuthenticated = () => {
  return new Auth().isAuthenticated();
}

if (!isAuthenticated())
  Redirect.toLogin();



ReactDOM.render(
  <React.StrictMode>
    <Router history={history}>
      <div>
        <Route render={(props)=> <Header {...props}/>} />

        <Route path="/" exact component={RecordsPage} />
        <Route path="/upload" exact component={FileUpload} />
        <Route path="/records/:id" exact component={RecordEditPage} />
        <Route path="/records/:id/actions" exact component={ActionsPage} />
        <Route path="/login" exact component={LoginPage} />
        <Route path="/users" exact component={UsersPage} />
        <Route path="/new/users" exact component={UserCreatePage} />
        <Route path="/users/:id" exact component={UserEditPage} />
       </div>
    </Router>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
