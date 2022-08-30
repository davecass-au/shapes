import React from 'react'
import Canvas from './Canvas'

export const ShapeBoard = ({ drawFunction }) => {
   return (
      <div className="shape-board">
         <Canvas draw={drawFunction} />
      </div>
   )
}