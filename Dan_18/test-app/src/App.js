import './App.css';

function MoviesHeader() {
  return (
    <tr>
      <th>Name</th>
      <th>Genre</th>
      <th>Year</th>
    </tr>
  );
}

function Movie(props) {
  return (
    <tr>
      <td>{props.name}</td>
      <td>{props.genre}</td>
      <td>{props.year}</td>
    </tr>
  );
}

function MovieTable() {
  return (
    <table className="movie-table">
      <MoviesHeader />
      <Movie name="Pulp Fiction" genre="Crime" year="1994" />
      <Movie name="Star Wars" genre="Sci-Fi" year="1977" />
      <Movie name="John Wick" genre="Thriller" year="2014" />
    </table>
  );
}

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Movies</h1>
        <MovieTable />
      </header>
    </div>
  );
}

export default App;
