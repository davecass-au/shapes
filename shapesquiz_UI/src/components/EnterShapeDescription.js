import React from 'react'

const EnterShapeDescription = ({ onDescriptionChanged }) => {

   return (
      <div className="container">
         <h5>Shape Description (e.g Draw a circle with a radius of 100 or Draw an octagon with a side length of 200 or Draw an oval with width of 200 and a height of 100.)</h5>
         <form>
            <div className="row">
               <input type="text" onChange={(e) => onDescriptionChanged(e)} className="form-control" name="description" id="description" placeholder="Shape to draw" />
            </div>                  
         </form>
      </div>
   )
}

export default EnterShapeDescription