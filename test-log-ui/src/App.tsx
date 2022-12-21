import FilterBar from './components/FilterBar'
import { Container } from 'reactstrap'
import NavBar from './components/NavBar'
import LogList from './components/LogList'

function App() {

  return (
    <Container fluid>
      <NavBar />
      <FilterBar />
      <LogList />
    </Container>
  )
}

export default App
