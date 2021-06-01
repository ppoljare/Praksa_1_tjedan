import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Button, FormGroup, Input, Label } from "reactstrap";
import axios from "axios";
import { action, makeObservable, observable } from "mobx";
import { observer } from "mobx-react";

const NewMovie = observer(
  class NewMovie extends Component {
    movieApi = "https://localhost:44354/api/movie/";
    newMovieData = { Name: '', Genre: '', YearReleased: '' };
    
    constructor(props) {
      super(props);
      
      this.newMovieData = { Name: '', Genre: '', YearReleased: '' };
      
      makeObservable(this, {
        newMovieData: observable,
        addMovie: action,
        setNewMovieData: action,
        setNewMovieDataAll: action
      });
    }
    
    async addMovie() {
      try {
        await axios.post(this.movieApi, this.newMovieData).then((response) => {
          this.setNewMovieDataAll({
            Name: '',
            Genre: '',
            YearReleased: ''
          });
          
          window.alert("Success!");
        });
      } catch (ex) {
        window.alert("Please enter valid data!");
      }
    }
    
    setNewMovieData(property, value) {
      switch(property) {
        case "Name":
          this.newMovieData.Name = value;
          break;
        case "Genre":
          this.newMovieData.Genre = value;
          break;
        case "YearReleased":
          this.newMovieData.YearReleased = value;
          break;
        default:
          break;
      }
    }
    
    setNewMovieDataAll(movie) {
      this.newMovieData = movie;
    }
    
    render() {
      return (
        <>
          <h1 className="my-3">Add new movie</h1>
          
          <FormGroup>
            <Label for="name">Name</Label>
            <Input id="name" value={this.newMovieData.Name} onChange={(e) => {
              this.setNewMovieData("Name", e.target.value);
            }} />
          </FormGroup>
          
          <FormGroup>
            <Label for="genre">Genre</Label>
            <Input id="genre" value={this.newMovieData.Genre} onChange={(e) => {
              this.setNewMovieData("Genre", e.target.value);
            }} />
          </FormGroup>
            
          <FormGroup>
            <Label for="year">Year</Label>
            <Input id="year" value={this.newMovieData.YearReleased} onChange={(e) => {
              this.setNewMovieData("YearReleased", e.target.value);
            }} />
          </FormGroup>
          
          <div className="my-3">
            
            <Button color="primary" onClick={() => {
              this.addMovie();
            }}>Add</Button>{' '}
            
            <Link to="/">
              <Button className="mx-3" color="secondary">Back</Button>
            </Link>
          </div>
        </>
      ); 
    }
  }
);

export default NewMovie;