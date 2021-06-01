import React, { Component } from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import EditMovie from "./components/EditMovie";
import MovieList from "./components/MovieList";
import NewMovie from "./components/NewMovie";

class App extends Component {
  render() {
    return (
      <div className="App container">
        
        <Router>
          <Switch>
            <Route exact path="/" component={MovieList} />
            <Route path="/new" component={NewMovie} />
            <Route path="/edit/:id" component={EditMovie} />
          </Switch>
        </Router>
        
      </div>
    );
  }
  
}

export default App;
