import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Button, Table } from "reactstrap";
import axios from "axios";
import { action, makeObservable, observable } from "mobx";
import { observer } from "mobx-react";

const MovieList = observer(
  class MovieList extends Component {
    movieApi = "https://localhost:44354/api/movie/";
    movies = [];
    sortBy;
    sortOrder;
    
    constructor(props) {
      super(props);
      
      let location = this.props.location;
      
      this.sortBy = location.sortBy;
      if (this.sortBy === "" || this.sortBy === undefined) {
        this.sortBy = "Name";
      }
      
      this.sortOrder = location.sortOrder;
      if (this.sortOrder === "" || this.sortOrder === undefined) {
        this.sortOrder = "asc";
      }
      
      makeObservable(this, {
        movies: observable,
        sortBy: observable,
        sortOrder: observable,
        deleteMovie: action,
        getMovies: action,
        changeSortParams: action,
        setMovies: action
      })
    }
    
    async componentDidMount() {
      await this.getMovies();
    }
    
    async deleteMovie(id) {
      if (window.confirm("Are you sure you want to delete this movie?")) {
        await axios.delete(this.movieApi + id).then((response) => {
          this.getMovies();
        });
      }
    }
    
    async getMovies() {
      let sortingParams = "SortBy=" + this.sortBy + "&SortOrder=" + this.sortOrder;
      
      await axios.get(this.movieApi + "?" + sortingParams).then((response) => {
        this.setMovies(response.data);
      });
    }
    
    setMovies(data) {
      this.movies = data;
    }
    
    changeSortParams = async (sortBy) => {
      if (this.sortBy === sortBy){
        if (this.sortOrder.toLowerCase() === "asc") {
          this.sortOrder = "desc";
        } else {
          this.sortOrder = "asc";
        }
      } else {
        this.sortBy = sortBy;
      }
      await this.getMovies();
    };

    render() {
      let movies = this.movies.map((movie) => {
        return (
          <tr key={movie.MovieId}>
            <td>{movie.Name}</td>
            <td>{movie.Genre}</td>
            <td>{movie.YearReleased}</td>
            <td>
              <Link to={{
                pathname: "/edit/" + movie.MovieId,
                movie: {
                  Name: movie.Name,
                  Genre: movie.Genre,
                  YearReleased: movie.YearReleased
                }
              }}>
                <Button color="success" size="sm">Edit</Button>
              </Link>
              
              <Button className="mx-2" color="danger" size="sm" onClick={() => this.deleteMovie(movie.MovieId)}>Delete</Button>
            </td>
          </tr>
        );
      });
      
      return (
        <>
          <h1 className="my-3">Movies App</h1>
          
          <Link to="/new">
            <Button className="my-3" color="primary">New movie</Button>
          </Link>
          
          <Table>
            <thead>
              <tr>
                <th>
                  <div>
                    Name
                    <Button className="mx-1" color="white" size="sm" onClick={() => this.changeSortParams("Name")}>Sort</Button>
                  </div>
                </th>
                <th>
                  <div>
                    Genre
                    <Button className="mx-1" color="white" size="sm" onClick={() => this.changeSortParams("Genre")}>Sort</Button>
                  </div>
                </th>
                <th>
                  <div>
                    Year
                    <Button className="mx-1" color="white" size="sm" onClick={() => this.changeSortParams("YearReleased")}>Sort</Button>
                  </div>
                </th>
                <th>Actions</th>
              </tr>  
            </thead>
            
            <tbody>
              {movies}
            </tbody>
          </Table>
        </>
      );
    }
  }
);

export default MovieList;