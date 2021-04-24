import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import reportWebVitals from './reportWebVitals';
import { Header } from './components/header/header';
import { FileUpload } from './pages/uploads/uploads-page';
import { RecordsPage } from './pages/records/records-page/records-page';
import { RecordEditPage } from './pages/records/record-edit-page/record-edit-page';

ReactDOM.render(
  <React.StrictMode>
    <Router>
      <div>
        <Header/>

        <Route path="/" exact component={RecordsPage} />
        <Route path="/upload" exact component={FileUpload} />
        <Route path="/records/:id" component={RecordEditPage} />
       </div>
    </Router>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
