import './App.css'
import Wrapper from './Wrapper/Wrapper.jsx'
import Screen from './Screen/Screen.jsx'
import ButtonBox from './ButtonBox/ButtonBox.jsx'
import Button from './Button/Button.jsx'

function App() {
  return (
    <>
      <Wrapper>
        <Screen value='0' />
        <ButtonBox>
          <Button className='' value='0' onClick={() => {}} />
        </ButtonBox>
      </Wrapper>
    </>
  )
}

export default App
