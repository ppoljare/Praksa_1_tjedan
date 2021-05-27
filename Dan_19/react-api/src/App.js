import React, { Component } from 'react';
import { Button, FormGroup, Input, Label, Modal, ModalHeader, ModalBody, ModalFooter, Table } from 'reactstrap';
import axios from 'axios';

class App extends Component {
  movieApi = 'https://localhost:44354/api/movie/';
  
  state = {
    movies: [],
    newMovieData: {
      Name: '',
      Genre: '',
      YearReleased: ''
    },
    editMovieData: {
      MovieId: '',
      Name: '',
      Genre: '',
      YearReleased: ''
    },
    newMovieModal: false,
    editMovieModal: false,
    sortBy: 'Name',
    sortOrder: 'ASC'
  }
  
  componentDidMount() {
    this._refreshMovies(this.state.sortBy, this.state.sortOrder);
  }
  
  toggleNewMovieModal() {
    this.setState({
      newMovieModal: !this.state.newMovieModal
    })
  }
  
  toggleEditMovieModal() {
    this.setState({
      editMovieModal: !this.state.editMovieModal
    })
  }
  
  _refreshMovies(sortBy, sortOrder) {
    axios.get(this.movieApi + '?SortBy=' + sortBy + '&SortOrder=' + sortOrder).then((response) => {
      this.setState({
        movies: response.data
      })
    });
  }
  
  changeSortParams(sortBy) {
    let sortOrder = this.state.sortOrder;
    if (this.state.sortBy === sortBy) {
      sortOrder = this.toggleSortOrder(sortOrder);
    } else {
      this.setState({sortBy: sortBy});
    }
    this._refreshMovies(sortBy, sortOrder);
  }
  
  toggleSortOrder() {
    if (this.state.sortOrder==='ASC') {
      this.setState({sortOrder: 'DESC'});
      return 'DESC';
    } else {
      this.setState({sortOrder: 'ASC'});
      return 'ASC';
    }
  }
  
  addMovie() {
    axios.post(this.movieApi, this.state.newMovieData).then((response) => {
      this._refreshMovies(this.state.sortBy, this.state.sortOrder);
      this.setState({
        newMovieModal: false,
        newMovieData: {
          Name: '',
          Genre: '',
          YearReleased: ''
        }
      });
    });
  }
  
  updateMovie() {
    let { Name, Genre, YearReleased } = this.state.editMovieData;
    axios.put(this.movieApi + this.state.editMovieData.MovieId, {
      Name, Genre, YearReleased
    }).then((response) => {
      this._refreshMovies(this.state.sortBy, this.state.sortOrder);
    })
    this.setState({
      editMovieModal: false, editMovieData: { MovieId: '', Name: '', Genre: '', YearReleased: '' }
    })
  }
  
  editMovie(id, name, genre, year) {
    this.setState({
      editMovieData: {
        MovieId: id,
        Name: name,
        Genre: genre,
        YearReleased: year
      },
      editMovieModal: ! this.editMovieModal
    });
  }
  
  deleteMovie(id) {
    axios.delete(this.movieApi + id).then((response) => {
      this._refreshMovies(this.state.sortBy, this.state.sortOrder);
    });
  }
  
  render(){
    let movies = this.state.movies.map((movie) => {
      return (
        <tr key={movie.MovieId}>
          <td>{movie.Name}</td>
          <td>{movie.Genre}</td>
          <td>{movie.YearReleased}</td>
          <td>
            <Button color="success" size="sm" onClick={this.editMovie.bind(this, movie.MovieId, movie.Name, movie.Genre, movie.YearReleased)}>Edit</Button>
            <Button className="mx-2" color="danger" size="sm" onClick={this.deleteMovie.bind(this, movie.MovieId)}>Delete</Button>
          </td>
        </tr>
      )
    });
    
    return (
      <div className="App container">
        
        <h1 className="my-3">Movies App</h1>
        
        <Button className="my-3" color="primary" onClick={this.toggleNewMovieModal.bind(this)}>New movie</Button>
        
        <Modal isOpen={this.state.newMovieModal} toggle={this.toggleNewMovieModal.bind(this)}>
          <ModalHeader toggle={this.toggleNewMovieModal.bind(this)}>Add a new movie</ModalHeader>
          
          <ModalBody>
            <FormGroup>
              <Label for="name">Name</Label>
              <Input id="name" value={this.state.newMovieData.Name} onChange={(e) => {
                let { newMovieData } = this.state;
                newMovieData.Name = e.target.value;
                this.setState({ newMovieData });
              }} />
            </FormGroup>
            
            <FormGroup>
              <Label for="genre">Genre</Label>
              <Input id="genre" value={this.state.newMovieData.Genre} onChange={(e) => {
                let { newMovieData } = this.state;
                newMovieData.Genre = e.target.value;
                this.setState({ newMovieData });
              }} />
            </FormGroup>
            
            <FormGroup>
              <Label for="year">Year</Label>
              <Input id="year" value={this.state.newMovieData.YearReleased} onChange={(e) => {
                let { newMovieData } = this.state;
                newMovieData.YearReleased = e.target.value;
                this.setState({ newMovieData });
              }} />
            </FormGroup>
          </ModalBody>
          
          <ModalFooter>
            <Button color="primary" onClick={this.addMovie.bind(this)}>Add</Button>{' '}
            <Button color="secondary" onClick={this.toggleNewMovieModal.bind(this)}>Cancel</Button>
          </ModalFooter>
        </Modal>
        
        <Modal isOpen={this.state.editMovieModal} toggle={this.toggleEditMovieModal.bind(this)}>
          <ModalHeader toggle={this.toggleEditMovieModal.bind(this)}>Edit movie data</ModalHeader>
          
          <ModalBody>
            <FormGroup>
              <Label for="name">Name</Label>
              <Input id="name" value={this.state.editMovieData.Name} onChange={(e) => {
                let { editMovieData } = this.state;
                editMovieData.Name = e.target.value;
                this.setState({ editMovieData });
              }} />
            </FormGroup>
            
            <FormGroup>
              <Label for="genre">Genre</Label>
              <Input id="genre" value={this.state.editMovieData.Genre} onChange={(e) => {
                let { editMovieData } = this.state;
                editMovieData.Genre = e.target.value;
                this.setState({ editMovieData });
              }} />
            </FormGroup>
            
            <FormGroup>
              <Label for="year">Year</Label>
              <Input id="year" value={this.state.editMovieData.YearReleased} onChange={(e) => {
                let { editMovieData } = this.state;
                editMovieData.YearReleased = e.target.value;
                this.setState({ editMovieData });
              }} />
            </FormGroup>
          </ModalBody>
          
          <ModalFooter>
            <Button color="primary" onClick={this.updateMovie.bind(this)}>Confirm</Button>{' '}
            <Button color="secondary" onClick={this.toggleEditMovieModal.bind(this)}>Cancel</Button>
          </ModalFooter>
        </Modal>
        
        <Table>
          <thead>
            <tr>
              <th>
                <div>
                  Name
                  <Button className="mx-1" color="white" size="sm" onClick={this.changeSortParams.bind(this, 'Name')}>Sort</Button>
                </div>
              </th>
              <th>
                <div>
                  Genre
                  <Button className="mx-1" color="white" size="sm" onClick={this.changeSortParams.bind(this, 'Genre')}>Sort</Button>
                </div>
              </th>
              <th>
                <div>
                  Year
                  <Button className="mx-1" color="white" size="sm" onClick={this.changeSortParams.bind(this, 'YearReleased')}>Sort</Button>
                </div>
              </th>
              <th>Actions</th>
            </tr>  
          </thead>
          
          <tbody>
            {movies}
          </tbody>
        </Table>
      </div>
    );
  }
}

export default App;
