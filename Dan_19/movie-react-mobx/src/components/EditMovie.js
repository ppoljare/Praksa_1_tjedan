import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Button, FormGroup, Input, Label } from "reactstrap";
import axios from "axios";
import { action, makeObservable, observable } from "mobx";
import { observer } from "mobx-react";

const EditMovie = observer(
  class EditMovie extends Component {
    movieApi = "https://localhost:44354/api/movie/";
    editMovieData = {
      MovieId: "",
      Name: "",
      Genre: "",
      YearReleased: ""
    };
    
    constructor(props) {
      super(props);
      
      let location = this.props.location;
      let pathname = location.pathname;
      let backslash = pathname.lastIndexOf("/");
      let MovieId = pathname.slice(backslash+1);
      
      makeObservable(this, {
        editMovieData: observable,
        updateMovie: action,
        setEditMovieData: action,
        setEditMovieDataAll: action
      });
      
      let movie = {
        MovieId: MovieId,
        Name: location.movie.Name,
        Genre: location.movie.Genre,
        YearReleased: location.movie.YearReleased
      };
      
      this.setEditMovieDataAll(movie);
    }
    
    async updateMovie() {
      let { MovieId, Name, Genre, YearReleased } = this.editMovieData;
      
      try {
        await axios.put(this.movieApi + MovieId, {
          Name: Name,
          Genre: Genre,
          YearReleased: YearReleased
        }).then((response) => {
          window.alert("Success!");
        });
      } catch (ex) {
        window.alert("Please enter valid data!");
      }
    }
      
    setEditMovieData(property, value) {
      switch(property) {
        case "MovieId":
          this.editMovieData.MovieId = value;
          break;
        case "Name":
          this.editMovieData.Name = value;
          break;
        case "Genre":
          this.editMovieData.Genre = value;
          break;
        case "YearReleased":
          this.editMovieData.YearReleased = value;
          break;
        default:
          break;
      }
    }
    
    setEditMovieDataAll(movie) {
      this.editMovieData = movie;
    }
    
    render() {
      return (
        <>
          <h1 className="my-3">Edit movie</h1>
          
          <FormGroup>
            <Label for="name">Name</Label>
            <Input id="name" value={this.editMovieData.Name} onChange={(e) => {
              this.setEditMovieData("Name", e.target.value);
            }} />
          </FormGroup>
          
          <FormGroup>
            <Label for="genre">Genre</Label>
            <Input id="genre" value={this.editMovieData.Genre} onChange={(e) => {
              this.setEditMovieData("Genre", e.target.value);
            }} />
          </FormGroup>
            
          <FormGroup>
            <Label for="year">Year</Label>
            <Input id="year" value={this.editMovieData.YearReleased} onChange={(e) => {
              this.setEditMovieData("YearReleased", e.target.value);
            }} />
          </FormGroup>
          
          <div className="my-3">
            
            <Button color="primary" onClick={() => {
              this.updateMovie();
            }}>Save</Button>{' '}
            
            <Link to="/">
              <Button className="mx-3" color="secondary">Back</Button>
            </Link>
          </div>
        </>
      ); 
    }
  }
);

export default EditMovie;