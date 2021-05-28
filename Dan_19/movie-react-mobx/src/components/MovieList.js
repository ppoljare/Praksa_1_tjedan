import React, { Component } from "react";
import { Button, FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader, Table } from "reactstrap";
import axios from "axios";
import { action, makeObservable, observable } from "mobx";
import { observer } from "mobx-react";

const MovieList = observer(
  class MovieList extends Component {
    movieApi = "https://localhost:44354/api/movie/";
    movies = [];
    sortBy;
    sortOrder;
    newMovieModal = false;
    newMovieData = { Name: '', Genre: '', YearReleased: '' };
    editMovieModal = false;
    editMovieData = { MovieId: '', Name: '', Genre: '', YearReleased: '' };
    
    constructor(props) {
      super(props);
      
      this.sortBy = this.props.sortBy;
      if (this.sortBy === "") {
        this.sortBy = "Name";
      }
      
      this.sortOrder = this.props.sortOrder;
      if (this.sortOrder === "") {
        this.sortOrder = "asc";
      }
      
      this.newMovieModal = false;
      this.newMovieData = { Name: '', Genre: '', YearReleased: '' };
      this.editMovieModal = false;
      this.editMovieData = { MovieId: '', Name: '', Genre: '', YearReleased: '' };
      
      makeObservable(this, {
        movies: observable,
        sortBy: observable,
        sortOrder: observable,
        newMovieModal: observable,
        newMovieData: observable,
        editMovieModal: observable,
        editMovieData: observable,
        addMovie: action,
        editMovie: action,
        deleteMovie: action,
        getMovies: action,
        updateMovie: action,
        changeSortParams: action,
        _setMovies: action,
        _setNewMovieModal: action,
        _setNewMovieData: action,
        _setEditMovieModal: action,
        _setEditMovieData: action,
        _setEditMovieDataAll: action
      })
    }
    
    toggleNewMovieModal = () => {
      this._setNewMovieModal(!this.newMovieModal);
    }
    
    toggleEditMovieModal = () => {
      this._setEditMovieModal(!this.editMovieModal);
    }
    
    async componentDidMount() {
      await this.getMovies();
    }
    
    async addMovie() {
      await axios.post(this.movieApi, this.newMovieData).then((response) => {
        this.getMovies();
        this._setNewMovieModal(false);
        this._setNewMovieData({
          Name: '',
          Genre: '',
          YearReleased: ''
        });
      });
    }
    
    async deleteMovie(id) {
      await axios.delete(this.movieApi + id).then((response) => {
        this.getMovies();
      });
    }
    
    editMovie(id, name, genre, year) {
      this._setEditMovieModal(!this.editMovieModal);
      this._setEditMovieDataAll({
        MovieId: id,
        Name: name,
        Genre: genre,
        YearReleased: year
      });
    }
    
    async getMovies() {
      let sortBy = this.sortBy;
      let sortOrder = this.sortOrder;
      await axios.get(this.movieApi + "?SortBy=" + sortBy + "&SortOrder=" + sortOrder).then((response) => {
        this._setMovies(response.data);
      });
    }
    
    async updateMovie() {
      let { Name, Genre, YearReleased } = this.editMovieData;
      await axios.put(this.movieApi + this.editMovieData.MovieId, {
        Name, Genre, YearReleased
      }).then((response) => {
        this.getMovies();
      })
      
      this._setEditMovieModal(false);
      this._setEditMovieData({
        MovieId: "",
        Name: "",
        Genre: "",
        YearReleased: ""
      });
    }
    
    _setMovies(data) {
      this.movies = data;
    }
    
    _setNewMovieModal(value) {
      this.newMovieModal = value;
    }
    
    _setEditMovieModal(value) {
      this.editMovieModal = value;
    }
    
    _setNewMovieData(property, value) {
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
    
    _setEditMovieData(property, value) {
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
    
    _setEditMovieDataAll(value) {
      this.editMovieData = value;
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
              <Button color="success" size="sm" onClick={() => this.editMovie(movie.MovieId, movie.Name, movie.Genre, movie.YearReleased)}>Edit</Button>
              <Button className="mx-2" color="danger" size="sm" onClick={() => this.deleteMovie(movie.MovieId)}>Delete</Button>
            </td>
          </tr>
        );
      });
      
      return (
        <>
          <Button className="my-3" color="primary" onClick={() => this.toggleNewMovieModal()}>New movie</Button>
          
          <Modal isOpen={this.newMovieModal} toggle={() => this.toggleNewMovieModal()}>
            <ModalHeader toggle={() => this.toggleNewMovieModal()}>Add a new movie</ModalHeader>
            
            <ModalBody>
              <FormGroup>
                <Label for="name">Name</Label>
                <Input id="name" value={this.newMovieData.Name} onChange={(e) => {
                  this._setNewMovieData("Name", e.target.value);
                }} />
              </FormGroup>
              
              <FormGroup>
                <Label for="genre">Genre</Label>
                <Input id="genre" value={this.newMovieData.Genre} onChange={(e) => {
                  this._setNewMovieData("Genre", e.target.value);
                }} />
              </FormGroup>
              
              <FormGroup>
                <Label for="year">Year</Label>
                <Input id="year" value={this.newMovieData.YearReleased} onChange={(e) => {
                  this._setNewMovieData("YearReleased", e.target.value);
                }} />
              </FormGroup>
            </ModalBody>
            
            <ModalFooter>
              <Button color="primary" onClick={() => this.addMovie()}>Add</Button>{' '}
              <Button color="secondary" onClick={() => this.toggleNewMovieModal()}>Cancel</Button>
            </ModalFooter>
          </Modal>
          
          <Modal isOpen={this.editMovieModal} toggle={() => this.toggleEditMovieModal()}>
            <ModalHeader toggle={() => this.toggleEditMovieModal()}>Update movie info</ModalHeader>
            
            <ModalBody>
              <FormGroup>
                <Label for="name">Name</Label>
                <Input id="name" value={this.editMovieData.Name} onChange={(e) => {
                  this._setEditMovieData("Name", e.target.value);
                }} />
              </FormGroup>
              
              <FormGroup>
                <Label for="genre">Genre</Label>
                <Input id="genre" value={this.editMovieData.Genre} onChange={(e) => {
                  this._setEditMovieData("Genre", e.target.value);
                }} />
              </FormGroup>
              
              <FormGroup>
                <Label for="year">Year</Label>
                <Input id="year" value={this.editMovieData.YearReleased} onChange={(e) => {
                  this._setEditMovieData("YearReleased", e.target.value);
                }} />
              </FormGroup>
            </ModalBody>
            
            <ModalFooter>
              <Button color="primary" onClick={() => this.updateMovie()}>Update</Button>{' '}
              <Button color="secondary" onClick={() => this.toggleEditMovieModal()}>Cancel</Button>
            </ModalFooter>
          </Modal>
          
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