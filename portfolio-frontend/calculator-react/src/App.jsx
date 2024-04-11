import './App.css'
import Wrapper from './Wrapper/Wrapper.jsx'
import Screen from './Screen/Screen.jsx'
import ButtonBox from './ButtonBox/ButtonBox.jsx'
import Button from './Button/Button.jsx'
import { buttonValues } from './constants/ButtonValuesConst.js'
import { useState } from 'react'

function App() {
  const [calc, setCalc] = useState({
    sign: '',
    num: 0,
    res: 0,
  })

  return (
    <>
      <Wrapper>
        <Screen value={calc.num ? calc.num : calc.res} />
        <ButtonBox>
          {buttonValues.flat().map((button, i) => {
            return (
              <Button
                key={i}
                className={button === '=' ? 'equals' : ''}
                value={button}
                onClick={
                  button === 'C'
                    ? resetClickHandler
                    : button === '+-'
                      ? invertClickHandler
                      : button === '='
                        ? equalsClickHandler
                        : button === '/' ||
                            button === 'X' ||
                            button === '-' ||
                            button === '+'
                          ? signCLickHandler
                          : button === '.'
                            ? commaClickHandler
                            : numClickHandler
                }
              />
            )
          })}
        </ButtonBox>
      </Wrapper>
    </>
  )
}

export default App
