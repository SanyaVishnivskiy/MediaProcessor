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

ReactDOM.render(
  <React.StrictMode>
    <Router history={history}>
      <div>
        <Header/>

        <Route path="/" exact component={RecordsPage} />
        <Route path="/upload" exact component={FileUpload} />
        <Route path="/records/:id" exact component={RecordEditPage} />
        <Route path="/records/:id/actions" exact component={ActionsPage} />
       </div>
    </Router>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
