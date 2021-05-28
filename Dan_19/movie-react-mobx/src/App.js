import React, { Component } from "react";
import MovieList from "./components/MovieList";

class App extends Component {
  render() {
    return (
      <div className="App container">
        <h1 className="my-3">Movies App</h1>
        
        <MovieList sortBy="" sortOrder="" />
      </div>
    );
  }
  
}

export default App;
