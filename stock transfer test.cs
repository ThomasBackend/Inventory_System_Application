[HttpPost]
        public IActionResult MoveStock(StockMovement obj)
        {
            if (ModelState.IsValid)
            {
                List<StockLevel> stocklevlObj = _db.StockLevel.ToList();
                List<WarehouseProduct> warehouseProductObj = _db.WarehouseProduct.ToList();
               int exists = 0;
foreach (var warehouseProduct in warehouseProductObj){
                  if( warehouseProduct.WarehouseId == obj.WarehouseFromId &&     warehouseProduct.ProductId == obj.ProductId){
                              exists = 1;
                              break;
                                  }
                            exists = 0;}
if(exists == 0){
                ModelState.AddModelError("WarehouseFromId", "Product does not exist in this warehouse");
                    return View();}  






if (stocklevlObj.Count == 0)
                {
                    ModelState.AddModelError("WarehouseFromId", "Warehouse has no Product");
                    return View();
                }







int sufficient = 0;
        foreach (var stock in stocklevlObj)
                {
                  if (stock.ProductId == obj.ProductId && stock.WarehouseId == obj.WarehouseFromId)
                    {
                     
                      if (obj.Qty > stock.QtyInStock){
                                ModelState.AddModelError("Qty", "Insufficient Product");
                                if(stock.QtyInStock == 0){
                                    foreach(var wp in warehouseProductObj)
                                {
                                    if (wp.ProductId == obj.ProductId)
                                    {
                                        _db.WarehouseProduct.Remove(wp);
                                    }
                                }
                      }
                                return view()
                                break;
                                }
                      if (stock.QtyInStock >= obj.Qty){
                        sufficient = 1;
                        break;
                      }
                      
                        }
                    }


int addable = 0;
foreach (var stock in stocklevlObj)
                    { 
                        if (stock.WarehouseId == obj.WarehouseToId)
                        {
                            stock.QtyInStock += obj.Qty;
                            _db.StockLevel.Update(stock);
                            addable = 1;
                            break;
                        }
                        addable= 0;
                    }

                                      
if (addable  == 0 )
                    {
                        StockLevel stocklevel = new()
                        {
                            QtyInStock = obj.Qty,
                            ProductId = obj.ProductId,
                            WarehouseId = obj.WarehouseToId
                        };
                        _db.StockLevel.Add(stocklevel);

                    }


int wpv = 0;
foreach(var wp in warehouseProductObj){
  if (wp.ProductId == obj.ProductId && wp.WarehouseId == obj.WarehouseToId)
                                    {
                                        wpv = 1;
                                        break;
                                    }
                                    wpv = 0;
}

if(wpv == 0){
  WarehouseProduct warehouseProduct = new (){
    WarehouseId = obj.WarehouseToId;
    ProductId = obj.ProductId;
  }
  _db.WarehouseProduct.Add(warehouseProduct);
}


int wtwp = 0;


foreach (var stock in stocklevlObj)
                    { 
                        if (stock.WarehouseId == obj.WarehouseToId)
                        {
                            if(stock.QtyInStock > 0){
                            wtwp = 1;
                            break;
                            }
                            break;
                        }
                        wtwp = 0;
                    }

if(wtwp == 0 ){
  foreach(var wp in warehouseProductObj)
                                {
                                    if (wp.ProductId == obj.ProductId && wp.WarehouseId == obj.WarehouseToId)
                                    {
                                        _db.WarehouseProduct.Remove(wp);
                                        break;
                                    }

                                }
}

if(exists == 1 && sufficient == 1){
  _db.StockMovement.Add(obj);
  _db.SaveChanges();
}


  
                  
                    return RedirectToAction("Index");
                }
            
        }
