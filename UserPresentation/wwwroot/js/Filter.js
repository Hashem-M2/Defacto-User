var GenderList = [];
var subCategoryList = [];
var sizesCodeList = [];
var colorCodeList = [];

var minPrice, maxPrice;
function checkGenderList(itm) {
    const index = GenderList.indexOf(itm);
    if (index !== -1) {
        GenderList.splice(index, 1); // Remove the item if it exists
    } else {
        GenderList.push(itm); // Add the item if it doesn't exist
    }
    ModelFilter()
    //console.log(GenderList)
}

function checkCategory(cat) {
    const index = subCategoryList.indexOf(cat);
    if (index !== -1) {
        subCategoryList.splice(index, 1); // Remove the item if it exists
    } else {
        subCategoryList.push(cat); // Add the item if it doesn't exist
    }
    ModelFilter()
  //  console.log(subCategoryList)
}
function checkSize(code) {
    const index = sizesCodeList.indexOf(code);
    if (index !== -1) {
        sizesCodeList.splice(index, 1); // Remove the item if it exists
    } else {
        sizesCodeList.push(code); // Add the item if it doesn't exist
    }
   ModelFilter()
   // console.log(sizesCodeList)
}
function checkColor(name) {
    const index = colorCodeList.indexOf(name);
    if (index !== -1) {
        colorCodeList.splice(index, 1); // Remove the item if it exists
    } else {
        colorCodeList.push(name); // Add the item if it doesn't exist
    }
    ModelFilter()
   //console.log(colorCodeList)
}


const rangeInput = document.querySelectorAll(".range-input input"),
    priceInput = document.querySelectorAll(".price-input input"),
    range = document.querySelector(".slider .progress");
let priceGap = 1000;

priceInput.forEach(input => {
    input.addEventListener("input", e => {
        let minPrice = parseInt(priceInput[0].value),
            maxPrice = parseInt(priceInput[1].value);

        if ((maxPrice - minPrice >= priceGap) && maxPrice <= rangeInput[1].max) {
            if (e.target.className === "input-min") {
                rangeInput[0].value = minPrice;
                range.style.left = ((minPrice / rangeInput[0].max) * 100) + "%";
            } else {
                rangeInput[1].value = maxPrice;
                range.style.right = 100 - (maxPrice / rangeInput[1].max) * 100 + "%";
            }
        }
    });
});

rangeInput.forEach(input => {
    input.addEventListener("input", e => {
        let minVal = parseInt(rangeInput[0].value),
            maxVal = parseInt(rangeInput[1].value);

        if ((maxVal - minVal) < priceGap) {
            if (e.target.className === "range-min") {
                rangeInput[0].value = maxVal - priceGap
            } else {
                rangeInput[1].value = minVal + priceGap;
            }
        } else {
            priceInput[0].value = minVal;
            priceInput[1].value = maxVal;
            range.style.left = ((minVal / rangeInput[0].max) * 100) + "%";
            range.style.right = 100 - (maxVal / rangeInput[1].max) * 100 + "%";
        }
        minPrice = minVal;
        maxPrice = maxVal;
        ModelFilter()
    });
   
});
function ModelFilter(PageNumber = 1) {
    $.ajax({
        url: "/product/ProductsFilterByAJax",
        type: "post",

        data: {
            PageNo:             PageNumber,
            genderArr:          GenderList,
            subCategoryArr:     subCategoryList,
            sizeArr:            sizesCodeList,
            colotArr:           colorCodeList,
            subCategory:        subCategory,
            searchTxt:          searchText,
            maxPrice:           maxPrice,
            mintPrice:          minPrice
        },
        success: function (data) {
            $('#partial').html(data);

        },
        error: function () {
            alert('Error');
        }
    });

}
