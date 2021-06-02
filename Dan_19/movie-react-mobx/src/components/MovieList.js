import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Button, Input, Table, Pagination, PaginationItem, FormGroup, Label } from "reactstrap";
import axios from "axios";
import { action, computed, makeObservable, observable } from "mobx";
import { observer } from "mobx-react";

const MovieList = observer(
  class MovieList extends Component {
    movieApi = "https://localhost:44354/api/movie/";
    movies = [];
    
    filteringParams;
    sortingParams;
    pagingParams;
    
    /******************************** CONSTRUCTOR *******************************/
    constructor(props) {
      super(props);
      
      let location = this.props.location;
      
      this.filteringParams = location.filteringParams;
      this.sortingParams = location.sortingParams;
      this.pagingParams = location.pagingParams;
      
      if (this.filteringParams === undefined) {
        this.filteringParams = {
          Name: "",
          Genre: "",
          YearLowerBound: "",
          YearUpperBound: ""
        };
      }
      
      if (this.sortingParams === undefined) {
        this.sortingParams = {
          SortBy: "Name",
          SortOrder: "asc"
        };
      }
      
      if (this.pagingParams === undefined) {
        this.pagingParams = {
          ItemsPerPage: 10,
          PageNumber: 1,
          TotalItems: 0
        }
      }
      
      makeObservable(this, {
        movies: observable,
        deleteMovie: action,
        getMovies: action,
        setMovies: action,
        
        filteringParams: observable,
        sortingParams: observable,
        pagingParams: observable,
        
        changeFilteringParams: action,
        changeSortingParams: action,
        changePagingParams: action,
        
        setTotalItems: action,
        totalPages: computed
      })
    }
    
    async componentDidMount() {
      await this.getMovies();
    }
    
    /****************************** CRUD OPERATIONS *****************************/
    async deleteMovie(id) {
      if (window.confirm("Are you sure you want to delete this movie?")) {
        await axios.delete(this.movieApi + id).then((response) => {
          this.getMovies();
        });
      }
    }
    
    async getMovies() {
      let filteringParams = this.generateFilteringString();
      let sortingParams = "SortBy=" + this.sortingParams.SortBy + "&SortOrder=" + this.sortingParams.SortOrder;
      let pagingParams = "ItemsPerPage=" + this.pagingParams.ItemsPerPage + "&Page=" + this.pagingParams.PageNumber;
      
      let queryString = this.movieApi + "?";
      
      if (filteringParams !== "") {
        queryString += filteringParams + "&";
      }
      
      queryString += sortingParams + "&" + pagingParams;
      
      await axios.get(queryString).then((response) => {
        this.setMovies(response.data.m_Item1);
        this.setTotalItems(response.data.m_Item2);
      });
    }
    
    setMovies(data) {
      this.movies = data;
    }
    
    /************************ FILTERING, SORTING, PAGING ************************/
    changeFilteringParams = async(filterBy, value) => {
      switch (filterBy) {
        case "Name":
          this.filteringParams.Name = value;
          break;
        case "Genre":
          this.filteringParams.Genre = value;
          break;
        case "YearLowerBound":
          this.filteringParams.YearLowerBound = value;
          break;
        case "YearUpperBound":
          this.filteringParams.YearUpperBound = value;
          break;
        default:
          break;
      }
      await this.getMovies();
    };
    
    changeSortingParams = async (sortBy) => {
      if (this.sortingParams.SortBy === sortBy){
        if (this.sortingParams.SortOrder.toLowerCase() === "asc") {
          this.sortingParams.SortOrder = "desc";
        } else {
          this.sortingParams.SortOrder = "asc";
        }
      } else {
        this.sortingParams.SortBy = sortBy;
      }
      await this.getMovies();
    };
    
    changePagingParams = async (itemToChange, value) => {
      switch (itemToChange) {
        case "ItemsPerPage":
          this.pagingParams.ItemsPerPage = value;
          break;
        case "PageNumber":
          this.pagingParams.PageNumber = value;
          break;
        default:
          break;
      }
      await this.getMovies();
    }
    
    setTotalItems(value) {
      this.pagingParams.TotalItems = value;
    }
    
    get totalPages() {
      if (this.pagingParams.ItemsPerPage === 0 || this.pagingParams.ItemsPerPage === "") {
        return 0;
      }
      return Math.ceil(this.pagingParams.TotalItems / this.pagingParams.ItemsPerPage);
    }

    generateFilteringString() {
      let filteringString = "";
      let params = this.filteringParams;
      
      if (params.Name !== "") {
        filteringString += "&Name=" + params.Name;
      }
      
      if (params.Genre !== "") {
        filteringString += "&Genre=" + params.Genre;
      }
      
      if (params.YearLowerBound !== 0) {
        filteringString += "&YearLowerBound=" + params.YearLowerBound;
      }
      
      if (params.YearUpperBound !== 0) {
        filteringString += "&YearUpperBound=" + params.YearUpperBound;
      }
      
      return filteringString;
    }
    
    /********************************** RENDER **********************************/
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
      
      let pageNumbers = Array.from({length: this.totalPages}, (x, i) => i+1);
      
      let pageButtons = pageNumbers.map((page) => {
        if (page === this.pagingParams.PageNumber) {
          return (
            <PaginationItem key={page}>
              <Button color="primary">{page}</Button>
            </PaginationItem> 
          );
        } else {
          return (
            <PaginationItem key={page}>
              <Button color="white" onClick={() => this.changePagingParams("PageNumber", page)}>{page}</Button>
            </PaginationItem> 
          );
        }
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
                    <Input placeholder="Name" id="name" value={this.filteringParams.Name} onChange={(e) => {
                      this.changeFilteringParams("Name", e.target.value);
                    }} />
                    Name
                    <Button className="mx-1" color="white" size="sm" onClick={() => this.changeSortingParams("Name")}>Sort</Button>
                  </div>
                </th>
                <th>
                  <div>
                    <Input placeholder="Genre" id="genre" value={this.filteringParams.Genre} onChange={(e) => {
                      this.changeFilteringParams("Genre", e.target.value);
                    }} />
                    Genre
                    <Button className="mx-1" color="white" size="sm" onClick={() => this.changeSortingParams("Genre")}>Sort</Button>
                  </div>
                </th>
                <th>
                  <div>
                    <Input placeholder="Year min" id="yearLowerBound" type="number" value={this.filteringParams.YearLowerBound} onChange={(e) => {
                      this.changeFilteringParams("YearLowerBound", e.target.value);
                    }} />
                    <Input placeholder="Year max" id="yearUpperBound" type="number" value={this.filteringParams.YearUpperBound} onChange={(e) => {
                      this.changeFilteringParams("YearUpperBound", e.target.value);
                    }} />
                    Year
                    <Button className="mx-1" color="white" size="sm" onClick={() => this.changeSortingParams("YearReleased")}>Sort</Button>
                  </div>
                </th>
                <th>Actions</th>
              </tr>  
            </thead>
            
            <tbody>
              {movies}
            </tbody>
          </Table>
          
          <Pagination>
            {pageButtons}
          </Pagination>
          
          <FormGroup>
            <Label for="itemsPerPage">Items per page</Label>
              <Input id="itemsPerPage" type="number" value={this.pagingParams.ItemsPerPage} onChange={(e) => {
              this.changePagingParams("ItemsPerPage", e.target.value);
            }} />
          </FormGroup>
          
        </>
      );
    }
  }
);

export default MovieList;