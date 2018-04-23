# How to keep proportional size for Bubbles corresponding to different series


<p>By default, each bubble weight is calculated in relation to other points shown in the same series:<br />      "Aaa": {4, 1, 5}<br />      "Bbb": {6, 9, 5}<br />      "Ccc": {4, 6, 5}<br /><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-keep-proportional-size-for-bubbles-corresponding-to-different-series-t180335/15.2.4+/media/8ee21674-792a-11e4-80ba-00155d624807.png"><br /> If you wish to represent each bubble size in relation to all other bubbles from all series you will need to update the entire series weight accordingly. This example demonstrates how to update <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraChartsBubbleSeriesView_MinSizetopic">BubbleSeries.View.MinSize</a> and <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraChartsBubbleSeriesView_MaxSizetopic">BubbleSeries.View.MaxSize</a> properties of all bubble series according to the min and max values of all series using the <a href="https://documentation.devexpress.com/#WindowsForms/DevExpressXtraChartsChartControl_BoundDataChangedtopic">BoundDataChanged Event</a>.<br /><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-keep-proportional-size-for-bubbles-corresponding-to-different-series-t180335/15.2.4+/media/996c65a8-792a-11e4-80ba-00155d624807.png"><br /><br /></p>

<br/>


