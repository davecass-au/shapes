import React, { Component } from 'react';
import { Header } from './components/Header';
import EnterShapeDescription from './components/EnterShapeDescription';
import { ShapeBoard } from './components/ShapeBoard';
import './index.css'
import './App.css'


export default class App extends Component {

   constructor(props) {
      super(props);
      this.state = { drawFunction: c => { }};
   }

   onDescriptionChanged = (e) => {         
      this.setState({ shapeDescription: e.target.value })
      this.buildDrawFunction(e.target.value);
   }

   static renderShape(drawFunction) {
      return (
         <ShapeBoard drawFunction={drawFunction} />
      );
   }

   render() {
      return (
         <div className="App">
            <Header/>
            <EnterShapeDescription onDescriptionChanged={this.onDescriptionChanged} />
            <ShapeBoard drawFunction={this.state.drawFunction}/>
         </div>
      );
   } 

   async buildDrawFunction(description) {
      const response = await fetch('api/shape?description=' + description);
      const data = await response.json();

      if (data.type === 1) {
         this.setState({
            drawFunction: (ctx, frameCount) => {
               ctx.fillStyle = '#000000'
               ctx.beginPath()
               ctx.ellipse(data.x, data.y, data.radiusX * Math.sin(frameCount * 0.05) ** 2, data.radiusY * Math.sin(frameCount * 0.05) ** 2, 0, 0, 2 * Math.PI)
               ctx.fill()
            }
         })                  
      }
      else if (data.type === 2) {
         this.setState({
            drawFunction: (ctx, frameCount) => {
               ctx.fillStyle = '#000000'
               ctx.beginPath()
               ctx.rect(data.x, data.y,data.width * Math.sin(frameCount * 0.05) ** 2, data.height * Math.sin(frameCount * 0.05) ** 2)
               ctx.fill()
            }
         })
      }
      else if (data.type === 3) {
         this.setState({
            drawFunction: (ctx, frameCount) => {
               ctx.fillStyle = '#000000'
               ctx.beginPath();
               ctx.moveTo(data.x + data.sideLength * Math.cos(0), data.y + data.sideLength * Math.sin(0));

               for (var i = 1; i <= data.numberOfSides; i += 1) {
                  ctx.lineTo(data.x + data.sideLength * Math.cos(i * 2 * Math.PI / data.numberOfSides) * Math.sin(frameCount * 0.05) ** 2, data.y + data.sideLength * Math.sin(i * 2 * Math.PI / data.numberOfSides) * Math.sin(frameCount * 0.05) ** 2);
               }
               ctx.closePath();
               ctx.fill();
            }
         })
      }
      else {
         this.setState({ drawFunction: c => { } })
      }
   }
}
